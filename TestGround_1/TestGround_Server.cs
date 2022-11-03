using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace TestGround_Server
{
    /**
     * 1. 기본형 제작 : 동기 서버 1:1 -> 1:N
     * 2. 비동기 함수를 이용한 서버 제작
     */
    internal class TestGround_Server
    {
        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);
            serverSocket.Bind(endPoint);

            serverSocket.Listen(100);
            while (true)
            {
                Socket client = serverSocket.Accept();

            }
        }
    }
}