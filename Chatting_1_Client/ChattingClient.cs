using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UserInfo;

namespace ChattingClient_1
{
    // 헤더의 역할
    public enum ePACKETTYPE
    {
        eWELCOME = 1000,
        eUSERINFO,
        eUSERCHAT
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

    public struct USERCHAT
    {
        public int userID;  // 서버에서 누가 보내는지 알려주는 아이디
        public string message;
    }

    class ChattingClient
    {
        //        static Socket userSock;
        static string strIp = "127.0.0.1";
        static int port = 8082;
        //static byte[] sendBuffer;
        //static byte[] receiveBuffer;
        static User user;
        static List<User> userList;
        static void Main(string[] args)
        {
            // 1. 소켓생성
            // 2. connet
            // 3. 안녕하세요 라는 메시지를 서버로 부터 수신 ( 할당받은 ID를 추가해서 전송 )
            // 4. 접속해 있는 사용자 정보를 서버로 부터 수신
            // 5. 자신이 작성한 메시지를 서버로 송신
            // 6. 서버로 부터 자신이 보낸 메시지를 수신하여 콘솔뷰에 출력
            // 7. 다른 사용자가 송신한 메시지를 콘솔뷰에 출력
            userList = new List<User>();
            user = new User(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIp), port);

            user.userSock.Connect(ip);
            user.userSock.Receive(user.receiveBuffer);  // 동기형태
            PacketParser();     // 웰컴메시지 받는 용도
            user.ClearReceiveBuffer();

            // 비동기 방식으로 수신 - 웰컴메시지 수신 계속 반복중
            user.Receive();
            string userMessage = string.Empty;
            while (!userMessage.Contains("!"))
            {
                userMessage = Console.ReadLine();
                byte[] _packetType = BitConverter.GetBytes((ushort)ePACKETTYPE.eUSERCHAT);
                byte[] msg = Encoding.Default.GetBytes(userMessage);
                Array.Copy(_packetType, 0, user.sendBuffer, 0, _packetType.Length);
                Array.Copy(msg, 0, user.sendBuffer, 6, msg.Length);
                user.Send();    // 입력한 메시지를 서버로 전송 (그냥 send하는게 아니라 패킷타입을 넣어줘야된다.)
                user.ClearSendBuffer();
            }
            user.Close();
        }
        public static void ReceiveCallBack(IAsyncResult ar)
        {
            PacketParser();
            user.Receive(); // 다른사람들의 메시지를 받기위해서 필요함.
        }
        public static void SendCallBack(IAsyncResult ar)
        {
            // 여기서는 다시 리시브 상태로 변환해야 한다.
            //user.Receive();
        }
        public static void PacketParser()
        {
            // 패킷을 분해
            byte[] _PACKETTYPE = new byte[2];
            Array.Copy(user.receiveBuffer, 0, _PACKETTYPE, 0, _PACKETTYPE.Length);
            short packetType = BitConverter.ToInt16(_PACKETTYPE, 0);
            switch (packetType)
            {
                case (int)ePACKETTYPE.eWELCOME:
                    {
                        byte[] _uid = new byte[4];          // id 는 정수형 이므로 4바이트
                        byte[] _message = new byte[122];    // 대화내용
                        Array.Copy(user.receiveBuffer, 2, _uid, 0, _uid.Length);
                        Array.Copy(user.receiveBuffer, 6, _message, 0, _message.Length);
                        int uid = BitConverter.ToInt32(_uid, 0);                // 서버에서 할당한 자신의 ID
                        string message = Encoding.Default.GetString(_message);  // 환영메시지
                        Console.WriteLine($"{uid}님 : {message}");
                    }
                    break;
                case (int)ePACKETTYPE.eUSERINFO:
                    {
                        byte[] _uid = new byte[4];                               // id 는 정수형 이므로 4바이트
                        Array.Copy(user.receiveBuffer, 2, _uid, 0, _uid.Length);
                        int uid = BitConverter.ToInt32(_uid, 0);                 // 서버에서 할당한 자신의 ID
                        User other = new User(uid);
                        userList.Add(other);
                        //Console.WriteLine(uid + " 님의 정보를 수신했습니다.");   -- 스트레스 테스트때문에 임시로 꺼둠
                    }
                    break;
                case (int)ePACKETTYPE.eUSERCHAT:
                    {
                        // 여기서 채팅 대화내용 처리
                        // Uid처리 - 받을 때 uid필요함(사람이 누군지알아아됨)
                        byte[] _uid = new byte[4];
                        Array.Copy(user.receiveBuffer, 2, _uid, 0, _uid.Length);    // 이때이미 0임
                        int uid = BitConverter.ToInt32(_uid, 0);
                        Console.WriteLine("UId는 " + uid);   // 왜 여기서 바뀌지?

                        byte[] _uMessage = new byte[122];   // 메시지는 122바이트만 받기
                        Array.Copy(user.receiveBuffer, 6, _uMessage, 0, user.receiveBuffer.Length - 6);
                        string message = Encoding.Default.GetString(_uMessage);
                        Console.WriteLine($"{uid}님의 대화 : {message}");
                    }
                    break;
            }
        }
    }
}
