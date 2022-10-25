using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_2_Thread_Server
{
    internal class Program
    {
        static Socket serverSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;
        static List<Socket> clientSockets = new List<Socket>();    //클라이언트 모음

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

            byte[] revBuffer = new byte[1024];
            byte[] sendBuffer = new byte[1024];
            while (!isInterrupt)
            {
                if (clientSockets.Count > 0)
                {
                    try
                    {
                        Array.Clear(revBuffer);
                        Array.Clear(sendBuffer);
                        //통신 - 1인통신
                        clientSockets[0].Receive(revBuffer);
                        string message = Encoding.Default.GetString(revBuffer);
                        sendBuffer = Encoding.Default.GetBytes(message);
                        Console.WriteLine(message + " " + sendBuffer.Length);
                        clientSockets[0].Send(sendBuffer);
                    }
                    catch (SocketException e)
                    {
                        // 소켓에러가 발생하면 클라이언트를 무조건 끊어주는 것이 좋다.
                        clientSockets[0].Shutdown(SocketShutdown.Both);
                        clientSockets[0].Close();
                        clientSockets.RemoveAt(0);
                        throw;
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
            Console.WriteLine("t1 인터럽트 호출");
        }

        /**
         * 이 함수의 용도는 클라이언트 접속만 관리하는 용도
         */
        static void NewClient()
        {
            serverSocket.Listen(100);
            while (!isInterrupt)
            {
                // 클라이언트 접속하면, 리스트에 등록하고
                Console.WriteLine("유저 대기중");
                Socket userSocket = serverSocket.Accept();
                Console.WriteLine($"유저 접속 : {userSocket.RemoteEndPoint}");
                if (!clientSockets.Contains(userSocket))
                {
                    clientSockets.Add(userSocket);
                }
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