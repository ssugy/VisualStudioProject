using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UserInfo;
using System.Runtime.InteropServices;

namespace ChattingServer_1
{
    public enum ePACKETTYPE
    {
        eWELCOME = 1000,
        eUSERINFO
    }
    public struct WELCOME
    {
        public int userID;
        public string message;
    }
    public struct USERINFO
    {
        public int userID;  // 서버에서 할당한 ID
    }

    class ChattingServer
    {
        static Socket listenSock;
        public static string strIp = "127.0.0.1";
        static int port = 8082;
        static Thread t1;
        static List<User> userList;
        static bool isInterrupt;
        static bool isInterruptMain;
        static void Main(string[] args)
        {
            userList = new List<User>();
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIp), port);
            listenSock.Bind(ip);        // Ip와 port를 할당
            Console.WriteLine("bind");
            ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            t1.Start();
            Console.WriteLine("쓰레드시작");
            t1.Join();
            t1.Interrupt();
        }
        static void NewClient()
        {
            while (!isInterrupt)
            {
                listenSock.Listen(100);                 // 소켓을 수신상태로 설정
                listenSock.BeginAccept(AcceptCallBack, null);
                Thread.Sleep(10);
            }
        }
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket userSock = listenSock.EndAccept(ar);
            User user = new User(userSock);
            Console.WriteLine("접속한사용자 = " + userSock.RemoteEndPoint + " ID = " + userSock.Handle);
            WelcomePacket(user);    // 안녕하세요 보내고
            SendUserInfos(user);    // 유저 정보를 서로 보내고 받고 처리 완료
            userList.Add(user);     // 리스트에 유저 정보를 넣고
            user.Receive();      // 여기서부터 채팅 대기 중.
        }
        static void WelcomePacket(User _user)
        {
            _user.ClearBuffer();
            // eWELCOME
            byte[] _PACKETTYPE = BitConverter.GetBytes((ushort)ePACKETTYPE.eWELCOME);
            byte[] _uid = BitConverter.GetBytes((int)_user.userSock.Handle);
            byte[] _message = Encoding.Default.GetBytes("안녕하세요.");
            // 버퍼에 복사 ( 이어서 ) ///////////////////////////
            Array.Copy(_PACKETTYPE, 0, _user.sendBuffer, 0, _PACKETTYPE.Length);
            Array.Copy(_uid, 0, _user.sendBuffer, 2, _uid.Length);
            Array.Copy(_message, 0, _user.sendBuffer, 6, _message.Length);
            _user.SendSyncronous();
        }
        static void SendUserInfos(User _user)
        {
            if (_user == null)
            {
                return;
            }

            try
            {
                _user.ClearBuffer();
                if (userList.Count > 0)
                {
                    // 1. 접속한 유저에게 다른 사람 정보를 전송
                    foreach (User one in userList)
                    {
                        // eWELCOME
                        byte[] _PACKETTYPE = BitConverter.GetBytes((ushort)ePACKETTYPE.eUSERINFO);
                        byte[] _uid = BitConverter.GetBytes((int)one.userSock.Handle);  // 접속한 유저들들의 핸들을 현재접속한 1명에게 보내는 것.
                        // 버퍼에 복사 ( 이어서 ) ///////////////////////////
                        Array.Copy(_PACKETTYPE, 0, _user.sendBuffer, 0, _PACKETTYPE.Length);
                        Array.Copy(_uid, 0, _user.sendBuffer, 2, _uid.Length);
                        _user.SendSyncronous();
                    }
                    // 2. 다른유저에게 현재 접속한 사람의 정보를 전송
                    foreach (User one in userList)
                    {
                        byte[] _PACKETTYPE = BitConverter.GetBytes((ushort)ePACKETTYPE.eUSERINFO);
                        byte[] _uid = BitConverter.GetBytes((int)_user.userSock.Handle);    // 현재 접속한 1명의 핸들을 여러 사람에게 보내기
                        Array.Copy(_PACKETTYPE, 0, one.sendBuffer, 0, _PACKETTYPE.Length);
                        Array.Copy(_uid, 0, one.sendBuffer, 2, _uid.Length);
                        one.SendSyncronous();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // 강사님은 여기서 패킷파서함수를 새로 만들어서 사용하심 - 나는 해당내용을 리시브콜백에 넣음.


        public static void ReceiveCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                if (userList.Count > 0)
                {
                    foreach (User one in userList)
                    {
                        if (one != null)
                        {
                            // 대화 메시지를 다른 사람들에게 모두 보내기.
                            one.CopySendBufFromOtherReceiveBuf(user.receiveBuffer);
                            byte[] uid = BitConverter.GetBytes((int)one.userSock.Handle);
                            Array.Copy(uid, 0, one.sendBuffer, 2, uid.Length); // Uid변경.
                            one.SendSyncronous();   //강사님이 이게 비동기가 맞다고 이야기하시니까 한번 바꿔보겠음 -> 안바꾸는걸로 함
                            //one.Send();       //비동기로 바꾸면 문제는 없지만, 종료되는 행위가 반복되는 부분이 있음. 이게 라우터를 여러개 거치면 중간에 반복되서 종료되는게 있음. 이게 에러는 없지만 별로같아서 동기로 바꿈
                        }
                    }
                }
                SendCallBack(ar);   // 여기서 유저 실행을 이어줌
            }
            catch (SocketException e)
            {
                try
                {
                    if (user != null && userList.Count > 0)
                    {
                        userList.Remove(user);
                        user.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    //중간에 접속 종료하면, 라우터를 여러개 거치면서 여러번 실행된다.
                }
            } 
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void SendCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                user.ClearSendBuffer();
                user.Receive();
            }
            catch (SocketException e)
            {
                userList.Remove(user);
                user.Close();
            }
        }
    }
}
