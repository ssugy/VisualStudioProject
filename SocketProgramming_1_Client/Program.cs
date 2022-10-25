using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketProgramming_1_Client
{
    internal class Program
    {
        static Socket clientSocket;
        //static string serverIP = "172.30.1.16";
        static string serverIP = "127.0.0.1";
        static int port = 8082;
        static void Main(string[] args)
        {
            try
            {
                // 1. 소켓 생성, 아이피 지정
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);   // 접속할 서버의 아이피와 포트 설정

                clientSocket.Connect(iPEndPoint);
                Console.WriteLine("커넥트 실행");

                /**
                 * 수신버퍼의 크기는 "길이-종류-메시지"이거의 종합 크기를 지정해줘야 된다.(x) 
                 * -> 테스트해보니, 종합크기가 아니라, 메시지 길이만큼만 있으면됨 (그러면 헤더크기+데이터 크기는 메모리에 임시로 저장되는건가..? 아니면
                 * -> 그런데 강사님이 이걸 다시 헤더크기 포함해야된다는 식으로 이야기하는 것 보니, 이거 왠지 시험문제 때문에 이야기하시는 것 같다.
                 * 그리고 11바이트의 데이터를 보내면 "11-01(타입)-메시지" 이렇게 수신이 되고,
                 * "메시지"부터 11바이트를 읽는다.
                 */
                byte[] recieveBuffer = new byte[1000];    // 수신버퍼의 크기 지정 (작으면 정상적인 데이터가 안됨)
                //clientSocket.Receive(recieveBuffer);
                //string receivedMessage = Encoding.Default.GetString(recieveBuffer);
                //Console.WriteLine(receivedMessage);
                ////recieveBuffer.Initialize(); // 초기화?
                //Array.Clear(recieveBuffer, 0, recieveBuffer.Length);    // 버퍼 초기화
                byte[] sendBuffer = new byte[1000];
                // 에코서버 만들기
                string userMessage = string.Empty;
                while (!userMessage.Contains("!"))
                {
                    userMessage = Console.ReadLine();
                    Console.WriteLine(userMessage);
                    sendBuffer = Encoding.Default.GetBytes(userMessage);
                    clientSocket.Send(sendBuffer);
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);

                    clientSocket.Receive(recieveBuffer);    // 서버에서 Send해야만 Receive
                    string receivedMessage = Encoding.Default.GetString(recieveBuffer);
                    Console.WriteLine($"받은 메시지 : {receivedMessage}");
                    Array.Clear(recieveBuffer, 0, recieveBuffer.Length);
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