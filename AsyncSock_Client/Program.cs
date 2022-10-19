using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncSock_Client
{
    internal class Program
    {
        static Socket clientSock;
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static byte[] rBuffer = new byte[256];

        static void Main(string[] args)
        {
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIP), port);

            clientSock.Connect(ip); //서버 연결요청
            clientSock.Receive(rBuffer);

            //바이트배열 문자로변환
            string Message = "hello world";
            byte[] data = Encoding.Default.GetBytes(Message);

            Array.Clear(rBuffer, 0, rBuffer.Length);

            //사용자 입력 받을 수 있도록
            while (true)
            {
                string message = Console.ReadLine();

                if (message.Length > 0)
                {
                    byte[] tmp = Encoding.Default.GetBytes(message);
                    clientSock.Send(tmp);
                }

            }
        }

        /// <summary>
        /// 비동기 서버 코드
        /// </summary>
        /// <param name="ar"></param>
        static void ReceivedCallback( IAsyncResult ar)
        {
            Socket userSock = (Socket)ar.AsyncState;
            Console.WriteLine();

            string Message = Encoding.Default.GetString(rBuffer);
            Console.WriteLine(Message);

            Array.Clear(rBuffer, 0, rBuffer.Length);

            userSock.BeginReceive(rBuffer, 0, rBuffer.Length, SocketFlags.None, ReceivedCallback, userSock);
        }

        static void SendCallBack(IAsyncResult ar)
        {
            Socket userSock = (Socket)ar.AsyncState;
            int sendByte = userSock.EndSend(ar);
        }
    }
}