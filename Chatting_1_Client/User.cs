﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using ChattingClient_1;

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
        public void Receive()
        {
            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, Program.ReceiveCallBack, this);
        }
        public void Send()
        {
            userSock.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, Program.SendCallBack, this);
        }
        public void ClearSendBuffer()
        {
            Array.Clear(sendBuffer, 0, sendBuffer.Length);
        }
        public void ClearReceiveBuffer()
        {
            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
        }
        public void ClearBuffers()
        {
            ClearSendBuffer();
            ClearReceiveBuffer();
        }
        public void CopySendBufFromReceiveBuf()
        {
            Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
        }
    }
}
