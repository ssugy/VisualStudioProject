

using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Net.Http;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace 시험용도
{
    internal class Program
    {

        static string strip = "";

        static void Main(string[] args)
        {
            Thread WorkThread = new Thread(AddUser);
            WorkThread.Start();
            WorkThread.Join();
            WorkThread.Interrupt();
            //
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);

            //
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strip), 8088);
            client.Connect(ip);

            // 4-2. 아래의 코드는 콘솔뷰에서 사용자로 부터 입력을 받는 부분이다. 빈칸에 알맞은 코드를 작성하시오.
            // 단, #을 입력받았을 경우 입력을 받지 않도록 해야 한다.
            string strMessage = "";
            while (!strMessage.Equals("#"))
            {
                strMessage = Console.ReadLine();
                Console.WriteLine("입력한 메시지 = " + strMessage);
            }

            // 5-1. 비동기 소켓함수로 데이터를 수신하는 함수를 작성하시오.
            // ( 소켓이름 sock, 배열이름은 rbuffer, 콜백함수이름 RecCallBack, 콜백함수 매개변수 Data클래스 인스턴스 user)
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] rbuffer = new byte[1024];
            User user = new User();
            sock.BeginReceive(rbuffer, 0, rbuffer.Length, SocketFlags.None, RecCallBack, user);
            sock.BeginSend(rbuffer, 0, rbuffer.Length, SocketFlags.None, SendDone, null);

            // 6-1.닷넷에서 제공하는 클라이언트, 서버 클래스 이름을 각각 나열하시오.
            //TcpListener
            //HttpListener
            //HttpClient

            // 아래의 코드는 서버에서 DB에 접속하는 프로그램 코드의 일부분이다.
            // UserInfo라는 테이블에서 DB정보를 가져오는 프로그램 코드를 완성하시오. (빈칸에 들어갈 내용을 순서대로 작성)
            string connetInfo = "Server=localhost;Database=game;Uid=testuser;Pwd=123;";
            //빈칸에 game이라는 데이터베이스에 포트 3306, 패스워드1234, uid root로 접속하도록 프로그램 코드를 완성하시오.(5점)
            string SQL서버정보 = "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=1234";

            using (MySqlConnection sqlcon = new MySqlConnection(connetInfo))
            {
                sqlcon.Open();
                string sql = "SELECT * FROM UserInfo";
                MySqlCommand cmd = new MySqlCommand(sql, sqlcon);
                MySqlDataReader r = cmd.ExecuteReader();
            }

            //
            //리스트에 저장된 유저리스트에서 특정 유저를 검색하는 프로그램 코드를 람다식을 사용하여 작성하시오.
            //특정유저의 데이터 형과 변수명은 Socket newClient이며 유저리스트는 다음과 같다. (12점)
            List<Socket> userlist = new List<Socket>();
            Socket newClient2;
            //Socket newClient = userlist.Find(x => x.Equals(newClient));
        }

        private static void RecCallBack(IAsyncResult ar)
        {
        }

        static void SendDone(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int nLen = client.SendBufferSize;
            Console.WriteLine("보낸버퍼길이 = " + nLen);
        }

        class User
        {

        }
      

        static public void AddUser()
        {

        }
    }
}
