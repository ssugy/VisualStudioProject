using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_1_Server
{
    internal class Program
    {
        static Socket listenSock;
        //static string strIP = "172.30.1.16";
        static string strIP = "127.0.0.1";
        static int port = 8082;

        // 접속한 유저를 리스트에 저장하기
        static List<Socket> userList;
        static void Main(string[] args)
        {
            // 1. 소켓 초기화, IP설정
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIP), port);   // ip와 port를 할당

            // 서버의 역할 - 클라이언트를 확인하면, 해당 소켓을 스레드에게 넘김.
            listenSock.Bind(ip);
            while (true)
            {
                listenSock.Listen(1000); // 소켓을 수신 상태로 설정
                Console.WriteLine("리슨 시작");
                Socket userSocket = listenSock.Accept();
                Console.WriteLine($"유저가 접속했습니다. {userSocket.RemoteEndPoint}");  //여기는 신호 올 때까지 대기, 상대방 ip와 포트 가져올수있음. RemoteEndPoint
                Thread echoTread = new Thread(ReturnMessage);
                echoTread.Start(userSocket);
            }
        }

        private static void ReturnMessage(object? obj)
        {
            while (true)
            {
                Socket clientSocket = obj as Socket;

                byte[] receiveBuffer = new byte[1000];
                byte[] sendBuffer = new byte[1000];
                clientSocket.Receive(receiveBuffer, SocketFlags.None);
                Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
                clientSocket.Send(sendBuffer);
            }
        }
    }
}