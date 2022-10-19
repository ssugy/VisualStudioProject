using System.Net.Sockets;
using System.Net;

namespace Network_1_Client
{
    internal class Program
    {
        static Socket clientSock;   //접속을 처리 할 소켓
        static string serverIP = "172.30.1.16"; // 서버와 통신 할 소켓
        static int port = 8082;

        static void Main(string[] args)
        {
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(serverIP), port);
            clientSock.Bind(ip);                    // 1. 바인드 함수 호출
            Console.WriteLine($"Bind 호출");
            clientSock.Listen(100);                 // 2. 리슨(접속요청 기다리는 수신대기), 100으로 설정하면 100명 까지 기다릴 수 있다.
            Console.WriteLine($"Listen 호출");

            Socket client = clientSock.Accept();    // 3.Accept: 클라이언트 접속 수락 허용 - Accept다음 코드가 실행 안된다.
            Console.WriteLine($"Accept 호출");
        }
    }
}