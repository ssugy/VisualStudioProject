using SocketProgramming_2_Thread_Server;
using System;
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
    public byte[] sendBuffer;
    public byte[] receiveBuffer;
    private const short MAXBUFSIZE = 128;
    private Thread t;

    public User(Socket _sock)
    {
        userSocket = _sock;
        sendBuffer = new byte[MAXBUFSIZE];
        receiveBuffer = new byte[MAXBUFSIZE];

        //유저마다 스레드 만들기
        ThreadStart threadStart = new ThreadStart(NewClient);
        t = new Thread(threadStart);
        t.Start();
    }

    public void NewClient()
    {
        try
        {
            while (true)
            {
                ClearReceiveBuffer();
                Receive();
                ClearSendBuffer();
                CopySendBufferFromReceiveBuffer();
                Send();
            }
        }
        catch (SocketException)
        {
            // 연결이 끊어지면 - 이거 서버측 화면에서 작동한다.
            Close();
        }

    }

    public void Close()
    {
        Console.WriteLine($"{userSocket.RemoteEndPoint}님이 접속을 종료하였습니다.");
        userSocket.Shutdown(SocketShutdown.Both);
        userSocket.Close();
    }

    public void ClearSendBuffer()
    {
        Array.Clear(sendBuffer);
    }

    public void ClearReceiveBuffer()
    {
        Array.Clear(receiveBuffer);
    }

    public void CopySendBufferFromReceiveBuffer()
    {
        Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);    // 이거 길이를 리시브 버퍼만큼해야되나? 둘중 짧은 길이로 해야되는거 아닌가.
    }

    public void Receive()
    {
        userSocket.Receive(receiveBuffer);
        SocketProgramming_2_Thread_Server.Program.MessageQueue.Enqueue(this);   // 리시브를 받으면 큐에 유저를 넣음.
    }

    public void Send()
    {
        userSocket.Send(sendBuffer);
    }
}
