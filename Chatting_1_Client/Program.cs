﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_1_Client
{
    public enum PACKET
    {
        eWELCOME = 1000,
        eCHATUSERS,
        eUSERINFO
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

    internal class Program
    {
        static string strIP = "127.0.0.1";
        static int port = 8082;
        static User user;   // 나 자신
        static List<User> userList = new List<User>();  // 다른사용자 저장용도
        static void Main(string[] args)
        {
            // 1. 소켓 생성 - User 클래스로 해결
            // 2. connet - User클래스로 해결
            // 3. 안녕하세요라는 메시지 수신(서버 ->클라이언트) - 할당받은 ID를 추가해서 서버로부터 전송 받음
            // 4. 접속해 있는 사용자 정보를 서버로 부터 수신
            // 4. 자신이 작성한 메시지를 서버로 송신(클라이언트 -> 서버)
            // 5. 서버로 부터 자신이 보낸 메시지를 수신하여 콘솔뷰에 출력
            // 6. 다른 사용자가 송신한 메시지를 콘솔뷰에 출력.
            user = new User(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIP),port);
            user.userSocket.Connect(ip);
            user.Receive(); // 동기 리시브라서 서버에서도 웰컴메시지는 일반으로 줘야됨

            // 패킷을 읽어서, 안녕하세요, 유저아이디를 구분해서 읽기 - PackParser
            //PacketParser();

            // 접속해 있는 사용자 정보를 서버로 부터 수신
            string userMessage = string.Empty;
            while (!userMessage.Contains("!"))
            {
                user.ClearSendBuffer();
                userMessage = Console.ReadLine();
                user.sendBuffer = Encoding.Default.GetBytes(userMessage);
                user.Send();
            }
            user.Close();
        }

        // 패킷 데이터를 우리 형식에 맞도록 처리하기.
        // 데이터를 자신의 구조에 맞도록 변환해 주는 것 : 파싱
        static public void PacketParser()
        {
            byte[] _PACKETTYPE = new byte[2];    //앞부분 2바이트를 가져온다. (구분용)
            Array.Copy(user.receiveBuffer, 0,_PACKETTYPE, 0, _PACKETTYPE.Length );  // 리시브 버퍼에 있는 내용을 패킷타입에 복사
            short packetType = BitConverter.ToInt16(_PACKETTYPE, 0);
            switch (packetType)
            {
                case (int)PACKET.eWELCOME:
                    byte[] _uid = new byte[4];          // ID는 정수형이므로 4바이트
                    byte[] _message = new byte[122];    // 128바이트에서 앞에 2바이트, 아이디4바이트 빼고나니 나머지 122바이트가 대화 내용
                    Array.Copy(user.receiveBuffer, 2, _uid, 0, _uid.Length);
                    Array.Copy(user.receiveBuffer, 6, _message, 0, _message.Length);
                    int uid = BitConverter.ToInt32(_uid, 0);                // 서버에서 할당한 나의 아이디
                    string message = Encoding.Default.GetString(_message);  // 환영메시지
                    Console.WriteLine($"유저아이디 : {uid}, 서버로부터 받은 메시지 : {message}");
                    break;
                case (int)PACKET.eCHATUSERS:
                    byte[] _charsMessage = new byte[122];
                    Array.Copy(user.receiveBuffer, 6, _charsMessage, 0, _charsMessage.Length);
                    Console.WriteLine($"{Encoding.Default.GetString(_charsMessage)}");
                    break;
                case (int)PACKET.eUSERINFO:
                    byte[] userInfo = new byte[4];
                    Array.Copy(user.receiveBuffer, 2, userInfo, 0, userInfo.Length);
                    int otherUId = BitConverter.ToInt32(userInfo, 0);
                    User otherUser = new User(otherUId);
                    userList.Add(otherUser);
                    Console.WriteLine($"유저인포 : {BitConverter.ToInt32(userInfo, 0)}");
                    break;
                default:
                    break;
            }
        }

        internal static void ReceiveCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            //Console.WriteLine(Encoding.Default.GetString(user.receiveBuffer));
            PacketParser();   //아직 이거는 서버에서 기능안만들어놓음.
        }

        internal static void SendCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            user.Receive();
        }
    }
}
