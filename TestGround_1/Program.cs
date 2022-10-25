using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestGround_1
{
    internal class Program
    {
        static Socket clientSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);

            // 2-1 클라이언트는 connet
            clientSocket.Connect(endPoint);
            Console.WriteLine("연결 완료");

            byte[] sendBuffer = new byte[1024];
            byte[] revBuffer = new byte[1024];
            while (true)
            {
                Array.Clear(sendBuffer);
                Array.Clear(revBuffer);

                // 에코서버 만들기 : 클라이언트가 메시지를 만들어서 보낸다. -> 서버가 그 메시지를 받아서 다시 클라이언트에게 보내준다. echo
                string message = Console.ReadLine();
                Console.WriteLine("내가 입력 한 메시지 : " + message);
                sendBuffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(sendBuffer);  // 1) 클라이언트가 데이터를 보냄.

                // 클라이언트가 메시지를 받는다.
                clientSocket.Receive(revBuffer);
                string revMessage = Encoding.Default.GetString(revBuffer);
                Console.WriteLine("서버에게서 받은 메시지 : " + revMessage);
            }
        }
    }
}