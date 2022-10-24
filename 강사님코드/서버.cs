using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace SocketProgramming_1_Server
{
    class Program
    {
        static Socket listenSock;
        static string strIp = "192.168.20.145";
        static int port = 8082;
        static List<Socket> userList;
        static void Main(string[] args)
        {
            userList = new List<Socket>();
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIp), port);
            listenSock.Bind(ip);        // Ip와 port를 할당
            Console.WriteLine("bind");
            listenSock.Listen(100);     // 소켓을 수신상태로 설정
            Console.WriteLine("listen");
            try
            {
                Socket userSock = listenSock.Accept();  // 새로 만든 연결에 대한 새 소켓을 할당
                Console.WriteLine("accept");
                Console.WriteLine("접속한유저 = " + userSock.RemoteEndPoint);
                string message = "안녕하세요.";
                byte[] tmp = Encoding.Default.GetBytes(message);
                userSock.Send(tmp);
                while (true)
                {
                    byte[] receiveBuffer = new byte[128];
                    byte[] sendBuffer = new byte[128];
                    userSock.Receive(receiveBuffer);
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);
                    Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
                    userSock.Send(sendBuffer);
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
            Console.WriteLine("프로그램종료");
        }
    }
}
