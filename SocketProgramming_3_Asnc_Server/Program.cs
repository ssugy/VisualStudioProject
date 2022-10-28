using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_3_Asnc_Server
{
    /// <summary>
    /// 클라이언트는 2_Thread_Client 그대로 사용 예정
    /// 유저를 구분하는 방법 : 소켓의 핸들로 구분한다.(소켓의 고유한 번호 = 핸들)
    /// </summary>
    internal class Program
    {
        static Socket listenSock;
        static string strIP = "127.0.0.1";
        static int port = 8082;
        static Thread t1;

        static byte[] sendBuffer;
        static byte[] receiveBuffer;

        static void Main(string[] args)
        {
            sendBuffer = new byte[128];
            receiveBuffer = new byte[128];

            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);

            listenSock.Bind(endPoint);
            listenSock.Listen(100);

            ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            Console.WriteLine("스레드 시작");
            t1.Start();

            // 종료
            t1.Join();
            t1.Interrupt();
        }

        // 비동기로 구현
        static void NewClient()
        {
            Console.WriteLine("유저 대기중..");  // 이거 이제 계속 나올 것 같은데..? 이거 이제 계속 체크함. 10ms마다 반복됨
            while (true)
            {
                listenSock.BeginAccept(AcceptCallBack, null);  // 마지막에 넣는것이 콜백함수의 파라미터임.
                Thread.Sleep(10);
            }
        }

        private static void AcceptCallBack(IAsyncResult ar)
        {
            Socket userSock = listenSock.EndAccept(ar); // 이게 Accept리턴값과 같음
            Console.WriteLine($"클라이언트 접속 : {userSock.RemoteEndPoint} // ID : {userSock.Handle}");

            // 안녕하세요 보내주기
            string message = "안녕하세요";
            byte[] sendBuffer = Encoding.Default.GetBytes(message);

            // user클래스 
            userSock.Send(sendBuffer);
            User user = new User(userSock);
            user.Receive();
            Thread.Sleep(10);
        }

        public static void ReceiveCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                user.CopySendBufferFromReceiveBuffer();
                user.Send();
            }
            catch (Exception)
            {
                user.Close();
            }
        }

        // 보내고 난 뒤에, 다시 리시브 상태로 변경
        public static void SendCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                user.ClearSendBuffer();
                user.Receive();
            }
            catch (Exception)
            {
                user.Close();
            }
        }
    }
}