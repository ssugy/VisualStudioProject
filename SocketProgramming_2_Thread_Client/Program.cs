using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_2_Thread_Client
{
    internal class Program
    {
        static Socket clientSocket;
        static string serverIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            // 1. 소켓 선언 및 엔드포인트 커넥트
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
            clientSocket.Connect(endPoint);

            while (true)
            {
                byte[] receivedBuffer = new byte[1024];
                clientSocket.Receive(receivedBuffer);
                string message = Encoding.Default.GetString(receivedBuffer);
                Console.WriteLine(message);

                // 에코 기능 구현
                byte[] sendBuffer = new byte[1024];
                string sendMsg = Console.ReadLine();
                sendBuffer = Encoding.Default.GetBytes(sendMsg);
                clientSocket.Send(sendBuffer);
                Console.WriteLine(sendBuffer + "전송완료");
            }
        }
    }
}