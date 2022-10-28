using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatting_1_Server
{
    internal class Program
    {
        static Socket listSocket;
        static string strIP = "127.0.0.1";
        static int port = 8082;

        static Thread t1;
        static public List<User> userList = new List<User>();
        static byte[] revBuffer = new byte[128];
        static byte[] sendBuffer = new byte[128];
        static void Main(string[] args)
        {
            listSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);
            listSocket.Bind(endPoint);
            listSocket.Listen(100);

            ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            t1.Start();

            t1.Join();
            t1.Interrupt();
        }

        static void NewClient()
        {
            Console.WriteLine("클라이언트 대기중");
            while (true)
            {
                listSocket.BeginAccept(AccetpCallback, null);
                Thread.Sleep(10);
            }
        }

        public enum PACKET
        {
            eWELCOME = 1000,
            eCHATUSERS, // 일반 문자 보낼 때
            eUSERINFO   // 유저 정보 보낼 때
        }

        // 패킷마다 구조체 필요
        public struct WELCOME
        {
            public int userID;
            public string message;
        }
        public struct USERINFO
        {
            // 서버에서 할당한 ID - 원래는 DB연동해서 회원가입하고 이런식으로 해야되는데 너무 커져서 지금 수업에서는 그렇게 안함.
            public int userID;
        }

        // 클라이언트와 동일해야 된다.
        // 타입별로 함수가 있으면 편하다
        static void WelcomePacket(User _user)
        {
            // eWelcome
            byte[] _Packet = BitConverter.GetBytes((ushort)PACKET.eWELCOME);
            byte[] _uid = BitConverter.GetBytes((int)_user.userSocket.Handle);
            byte[] _message = Encoding.Default.GetBytes("안녕하세요");
            // 버퍼에 복사
            Array.Copy(_Packet, 0, _user.sendBuffer, 0, _Packet.Length);
            Array.Copy(_uid, 0, _user.sendBuffer, 2, _uid.Length);
            Array.Copy(_message, 0, _user.sendBuffer, 6, _message.Length);
            _user.SendSyncronous(); // 동기로 보내기
            _user.ClearBuffer();
        }

        //일반 문자 보내는 용도(에코기능)
        static void SendCharcters(User user)
        {
            // eWelcome
            byte[] _Packet = BitConverter.GetBytes((ushort)PACKET.eCHATUSERS);
            byte[] _uid = BitConverter.GetBytes((int)user.userSocket.Handle);
            // 버퍼에 복사
            Array.Copy(_Packet, 0, user.sendBuffer, 0, _Packet.Length);
            Array.Copy(_uid, 0, user.sendBuffer, 2, _uid.Length);
            Array.Copy(user.receiveBuffer, 0, user.sendBuffer, 6, user.sendBuffer.Length - 6);  // 받은 메시지를 뒤에 이어서 붙이기
            user.SendSyncronous();
            user.ClearBuffer();
        }

        /**
         * 이게 자신의 정보를 전달하는게 아니라, 유저에게 다른 사용자 정보를 전달하게 한다 -> 유저는 다른사용자 정보를 저장하고 있다.
         */
        static void SendUserInfos(User _user)
        {
            // 1. 접속한 유저에게 다른 사람 정보를 전송해준다.
            // 2. 다른 유저에게 현재 접속한 사람을 전송 - 이거 나중에 처리
            // 다른사람정보를 보낼 때 까지는 동기방식으로 보내라 -> 왜인지 정확히 모름.
            foreach (User client in userList)
            {
                // eWelcome
                byte[] _Packet = BitConverter.GetBytes((ushort)PACKET.eUSERINFO);
                byte[] _uid = BitConverter.GetBytes((int)client.userSocket.Handle);
                // 버퍼에 복사
                Array.Copy(_Packet, 0, _user.sendBuffer, 0, _Packet.Length);
                Array.Copy(_uid, 0, _user.sendBuffer, 2, _uid.Length);
                _user.SendSyncronous();
                _user.ClearBuffer();
            }
        }

        private static void AccetpCallback(IAsyncResult ar)
        {
            Socket client = listSocket.EndAccept(ar);
            Console.WriteLine($"유저가 접속했습니다. {client.RemoteEndPoint}");
            User user = new User(client);
            userList.Add(user);

            // 안녕하세요 메시지 보내기
            WelcomePacket(user);    // 여기서 send처리함(패킷을 만들고 send함)
            SendUserInfos(user);
            user.Receive();
        }

        internal static void SendCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                user.Receive();
            }
            catch (SocketException)
            {
                if (userList.Contains(user))
                {
                    userList.Remove(user);
                }
            }
            catch (ObjectDisposedException)
            {

            }
        }

        internal static void ReceiveCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            try
            {
                SendCharcters(user);    //에코기능 - 정상작동함
            }
            catch (SocketException)
            {   
            }
            catch (ObjectDisposedException)
            {

            }
        }
    }
}
