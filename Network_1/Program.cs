using System.Net;
using System.Net.Sockets;

/**
 * 소켓 만들어 보기
 */
namespace Network_1
{
    internal class Program
    {
        static Socket listenSock;   //접속을 처리 할 소켓
        static string serverIP = "172.30.1.16";
        static int port = 8082;

        static void Main(string[] args)
        {
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(serverIP), port);
            listenSock.Bind(ip);                    // 1. 바인드 함수 호출
            Console.WriteLine($"Bind 호출");
            listenSock.Listen(100);                 // 2. 리슨(접속요청 기다리는 수신대기), 100으로 설정하면 100명 까지 기다릴 수 있다.
            Console.WriteLine($"Listen 호출");
            
            Socket client = listenSock.Accept();    // 3.Accept: 클라이언트 접속 수락 허용 - Accept다음 코드가 실행 안된다.
            Console.WriteLine($"Accept 호출");
            Console.WriteLine($"접속한 클라이언트 IP : {client.RemoteEndPoint.ToString()}");

            // 접속한 클라이언트에게 메시지를 전송
            string Message = "연결이 성공했습니다.";

            //문자열을 바이트 배열로 변환
            byte[] data = System.Text.Encoding.Default.GetBytes(Message);

            
        }
    }
}