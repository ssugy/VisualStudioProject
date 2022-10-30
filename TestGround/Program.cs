using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestGround_Client
{
    internal class Program
    {
        static Socket serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);
            serverSock.Connect(endPoint);
            Console.WriteLine("서버로 접속시도완료");

            while (true)
            {
                byte[] revBuffer = new byte[128];
                byte[] sendBuffer = new byte[128];
                serverSock.Receive(revBuffer);
                Console.WriteLine(Encoding.Default.GetString(revBuffer));
                string message = Console.ReadLine();
                sendBuffer = Encoding.Default.GetBytes(message);
                serverSock.Send(sendBuffer);
            }
        }
    }
}