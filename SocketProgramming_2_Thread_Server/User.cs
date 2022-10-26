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
        t.Start();  // 여기에서 어차피 무한루프로 돌고 있기 때문에 바로 조인/인터럽트로 넘어가지 않음.

        //t.Join();   // 정상적으로 스레드를 종료하기 위한 코드
        //t.Interrupt();
        //Console.WriteLine("test");
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

            Console.WriteLine("스레드 종료 코드 실행");
            t.Join();   // 스레드 종료하기
            t.Interrupt();  // 이렇게 해야지 스레드 정상종료 될 것이라고 봄. 인터럽트 이후에는 아무것도 실행 안된다.
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
