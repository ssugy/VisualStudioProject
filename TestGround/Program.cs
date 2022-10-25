using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestGround
{
    internal class Program
    {
        static Socket serverSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            // 1. 소켓 생성, ip, port연결
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);

            // 2.(서버인경우) bind, listen, Accept까지 진행
            // 2-1 (클라이언트) Connet
            serverSocket.Bind(endPoint);
            serverSocket.Listen(100);   // 최대 접속 인원x(서버 수용량에 따라 다르다), 최대 대기 인원(o)
            Console.WriteLine("리슨까지 실행");
            Socket client = serverSocket.Accept();  // 클라이언트 대기 상태
            Console.WriteLine($"클라이언트가 접속함 : {client.RemoteEndPoint}");

            byte[] revBuffer = new byte[1024];
            byte[] sendBuffer = new byte[1024];
            while (true)
            {
                Array.Clear(sendBuffer);
                Array.Clear(revBuffer);

                // 2) 서버가 클라이언트가 보낸 데이터를 받는다. 서버가 확인한다.
                client.Receive(revBuffer);
                string message = Encoding.Default.GetString(revBuffer);
                Console.WriteLine("클라이언트에게서 받은 메시지 : " + message);

                // 2-1) 클라이언트에게 받은 메시지 그대로를 다시 보내준다.
                sendBuffer = Encoding.Default.GetBytes(message);
                client.Send(sendBuffer);
            }
        }
    }
}