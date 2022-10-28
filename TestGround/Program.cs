using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestGround_Client
{
    internal class Program
    {
        static Socket clientSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(strIP, port);

            Console.WriteLine("서버 연결완료");
            byte[] revBuffer = new byte[128];
            clientSocket.Receive(revBuffer);    // 동기함수 : Receive, 비동기? 

            // 여기는 기본적으로 리시브 받고 실행된다는 보장이 있어요. 왜? 리시브 함수가 바로 위에 있어서 그렇다.
            Console.WriteLine(Encoding.Default.GetString(revBuffer));
        }
    }
}