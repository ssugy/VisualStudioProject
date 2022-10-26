using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_3_Asnc_Server
{
    /// <summary>
    /// 클라이언트는 2_Thread_Client 그대로 사용 예정
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
            while (true)
            {
                Console.WriteLine("유저 대기중..");  // 이거 이제 계속 나올 것 같은데..? 이거 이제 계속 체크함. 10ms마다 반복됨
                listenSock.BeginAccept(AcceptCallBack, null);   // 마지막에 넣는것이 콜백함수의 파라미터임.
                Thread.Sleep(1000);
            }
        }

        private static void AcceptCallBack(IAsyncResult ar)
        {
            Socket userSock = listenSock.EndAccept(ar); // 이게 Accept리턴값과 같음
            Console.WriteLine($"클라이언트 접속 : {userSock.RemoteEndPoint}");

            // 안녕하세요 보내주기
            string message = "안녕하세요";
            byte[] sendBuffer = Encoding.Default.GetBytes(message);
            userSock.Send(sendBuffer);
            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length,SocketFlags.None, ReceiveCallBack, userSock);  //마지막에 넣는 것은 콜백함수의 파라미터.
            Thread.Sleep(10);
        }

        private static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket userSock = ar.AsyncState as Socket;  // 이렇게 하는게 오브젝트를 넘겨서 받는 방법.
            Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
            Array.Clear(receiveBuffer);
            userSock.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, SendCallBack, userSock);
        }

        // 보내고 난 뒤에, 다시 리시브 상태로 변경
        private static void SendCallBack(IAsyncResult ar)
        {
            Socket userSock = ar.AsyncState as Socket;
            int sendLength = userSock.EndSend(ar);  //보내고 난뒤, 크기가 얼마인 데이터를 보냈는지 확인 가능
            Array.Clear(sendBuffer);    // send버퍼 초기화
            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ReceiveCallBack, userSock);
        }
    }
}