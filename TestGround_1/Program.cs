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
    internal class Program
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
                Console.WriteLine("접속 대기 중");
                Socket clientSock = serverSocket.Accept();
                Console.WriteLine($"클라이언트 접속 : {clientSock.RemoteEndPoint}");

                string welcomeMsg = "안녕하세요";
                byte[] sendBuffer = new byte[128];
                sendBuffer = Encoding.Default.GetBytes(welcomeMsg);
                clientSock.Send(sendBuffer);

                while (true)
                {
                    try
                    {
                        // 에코 기능 구현
                        byte[] revBuffer = new byte[128];
                        clientSock.Receive(revBuffer);
                        Array.Clear(sendBuffer);
                        sendBuffer = revBuffer;
                        clientSock.Send(sendBuffer);
                    }
                    catch (SocketException)
                    {
                        // 클라이언트가 연결을 끊으면, 
                        clientSock.Shutdown(SocketShutdown.Both);
                        clientSock.Close();
                    }catch (ObjectDisposedException)
                    {
                        Console.WriteLine("오브젝트 익셉션 발생");
                        break;
                    }
                }
            }
        }
    }
}