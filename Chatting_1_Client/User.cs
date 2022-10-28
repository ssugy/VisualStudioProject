﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 소켓, 송신버퍼, 수신버퍼
/// </summary>
public class User
{
    public Socket userSocket;
    public int socketHandle;
    public byte[] sendBuffer;
    public byte[] receiveBuffer;
    private const short MAXBUFSIZE = 128;

    // 기본생성자는 자신만 생성
    public User(int _socketHandle)
    {
        socketHandle = _socketHandle;
        sendBuffer = new byte[MAXBUFSIZE];
        receiveBuffer = new byte[MAXBUFSIZE];
    }

    public User(Socket _sock)
    {
        userSocket = _sock;
        sendBuffer = new byte[MAXBUFSIZE];
        receiveBuffer = new byte[MAXBUFSIZE];
    }

    public void Close()
    {
        Console.WriteLine($"{userSocket.RemoteEndPoint}님이 접속을 종료하였습니다.");
        userSocket.Shutdown(SocketShutdown.Both);
        userSocket.Close();
    }

    public void ClearSendBuffer()
    {
        Array.Clear(sendBuffer, 0, sendBuffer.Length);
    }

    public void ClearReceiveBuffer()
    {
        Array.Clear(receiveBuffer, 0, sendBuffer.Length);
    }

    public void CopySendBufferFromReceiveBuffer()
    {
        Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);    // 이거 길이를 리시브 버퍼만큼해야되나? 둘중 짧은 길이로 해야되는거 아닌가.
    }

    public void Receive()
    {
        userSocket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, Chatting_1_Client.Program.ReceiveCallBack, this);
    }

    public void Send()
    {
        userSocket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, Chatting_1_Client.Program.SendCallBack, this);
    }

   public void ClearBuffer()
    {
        ClearReceiveBuffer();
        ClearSendBuffer();
    }
}
