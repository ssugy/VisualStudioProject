using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_2_Thread_Server
{
    /// <summary>
    /// 하나둘셋넷
    /// </summary>
    internal class Program
    {
        static Socket serverSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;
        static List<User> userList = new List<User>();    //클라이언트 모음
        static public Queue<User> MessageQueue = new Queue<User>();    // 메시지용 큐

        static Thread t1;
        static bool isInterrupt;
        static void Main(string[] args)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);
            serverSocket.Bind(endPoint);
            Console.WriteLine("바인드 호출");

            ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            t1.Start(); //여기 사용하는 방법 실수함 스레드를 먼서 생성해야된다.

            

            while (!isInterrupt)
            {
                if (userList.Count > 0)
                {
                    try
                    {
                        
                    }
                    catch (SocketException e)
                    {
                    }
                    catch (ObjectDisposedException e)
                    {
                        Console.WriteLine(e.Message);
                    } 
                    finally
                    {
                    }
                }
            }
            t1.Join();
            t1.Interrupt();
            Console.WriteLine("서버 종료");
        }

        /// <summary>
        /// 이 함수의 용도는 클라이언트 접속만 관리하는 용도
        /// </summary>
        static void NewClient()
        {
            serverSocket.Listen(100);
            while (!isInterrupt)
            {
                // 클라이언트 접속하면, 리스트에 등록하고
                Console.WriteLine("유저 대기중");
                Socket userSocket = serverSocket.Accept();
                Console.WriteLine($"유저 접속 : {userSocket.RemoteEndPoint}");
                User user = new User(userSocket);   // 이때 이미 스레드 처리 시작함.
                Thread.Sleep(10);

                // 안녕하세요 메시지 보내기
                string message = "안녕하세요";
                byte[] sendBuffer = new byte[1000];
                sendBuffer = Encoding.Default.GetBytes(message);
                userSocket.Send(sendBuffer);
                Thread.Sleep(10);
            }
        }
    }
}