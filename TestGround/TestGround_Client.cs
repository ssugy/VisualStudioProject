using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestGround_Client
{
    internal class TestGround_Client
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
               
            }
        }
    }
}