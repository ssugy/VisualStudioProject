using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_1_Server
{
    internal class Program
    {
        static Socket listenSock;
        static string strIP = "172.30.1.16";
        //static string strIP = "127.0.0.1";
        static int port = 8082;

        // 접속한 유저를 리스트에 저장하기
        static List<Socket> userList;
        static void Main(string[] args)
        {
            userList = new List<Socket>();
            // 1. 소켓 초기화, IP설정
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIP), port);   // ip와 port를 할당

            // 서버의 역할
            listenSock.Bind(ip); 
            Console.WriteLine(ip);
            listenSock.Listen(1000); // 소켓을 수신 상태로 설정
            Console.WriteLine("리슨 시작");
            // Accept의 반환값은 클라이언트 소켓
            Socket userSocket = listenSock.Accept(); // 여기서 서버는 대기(클라이언트 접속 신호 올 때 까지)
            //userList.Add(userSocket);   // 리스트에 접속한 유저 추가
            Console.WriteLine($"유저가 접속했습니다. {userSocket.RemoteEndPoint}");  //여기는 신호 올 때까지 대기, 상대방 ip와 포트 가져올수있음. RemoteEndPoint

            //// 접속한 클라이언트에게 메시지 보내기
            //string message = "안녕하세요";
            //byte[] sendBuffer = new byte[1000];
            //sendBuffer = Encoding.Default.GetBytes(message); // 데이터 전송은 바이트 배열로 보내야됨
            //userSocket.Send(sendBuffer);
            //Console.WriteLine("데이터 전송 완료");
            //Array.Clear(sendBuffer, 0, sendBuffer.Length);  // 센드버퍼 초기화
            //// 무한루프를 통해서 서버가 상시 대기하도록 하기
           
            try
            {
                while (true)
                {
                    // 접속한 클라이언트가 보낸 메시지 확인 및 다시 보내기 - 리시브하고, 그 뒤 인코딩
                    byte[] receiveBuffer = new byte[1000];
                    byte[] sendBuffer = new byte[1000];
                    userSocket.Receive(receiveBuffer);
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);
                    Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
                    userSocket.Send(sendBuffer);
                    Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            }

        }
    }
}