using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using ChattingServer_1;
namespace UserInfo
{
    class User
    {
        // 소켓
        // 송신버퍼
        // 수신버퍼
        public Socket userSock;
        public int sockHandle;
        public byte[] sendBuffer;
        public byte[] receiveBuffer;
        private const short MAXBUFSIZE = 128;
        public User(int _sockHandle)
        {
            sockHandle = _sockHandle;
            sendBuffer = new byte[MAXBUFSIZE];
            receiveBuffer = new byte[MAXBUFSIZE];
        }
        public User(Socket _sock)
        {
            userSock = _sock;
            sendBuffer = new byte[MAXBUFSIZE];
            receiveBuffer = new byte[MAXBUFSIZE];
        }
        public void Close()
        {
            Console.WriteLine(userSock.RemoteEndPoint + " 님이 접속종료했습니다.");
            userSock.Shutdown(SocketShutdown.Both);
            userSock.Close();
        }
        
        // 소켓익셉션에 로그찍으면 너무많이 나와서 우선 안찍음
        public void SendSyncronous()
        {
            try
            {
                userSock.Send(sendBuffer);
            }
            catch (SocketException)
            {

            }
        }
        public void Receive()
        {
            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ChattingServer.ReceiveCallBack, this);
        }
        public void Send()
        {
            userSock.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, ChattingServer.SendCallBack, this);
        }
        public void ClearSendBuffer()
        {
            Array.Clear(sendBuffer, 0, sendBuffer.Length);
        }
        public void ClearReceiveBuffer()
        {
            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
        }
        public void ClearBuffer()
        {
            Array.Clear(sendBuffer, 0, sendBuffer.Length);
            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
        }
        public void CopySendBufFromReceiveBuf()
        {
            Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
        }

        // 버퍼 복사를 외부의 값으로 지정하는 함수가 필요
        public void CopySendBufFromOtherReceiveBuf(byte[] otherBuffer)
        {
            Array.Copy(otherBuffer, sendBuffer, otherBuffer.Length);
        }

        // 서버와 클라이언트 둘다 디버그 연속 추적하는게 쉽지않아서? 
        public void ShowBufferLog()
        {

        }
    }
}
