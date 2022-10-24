// 서버 역할

using System.Net;
using System.Net.Sockets;

string strIP = "127.0.0.1";
int port = 8082;

// 1. 소켓 생성
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

// 2. 아이피엔드포인트 지정
IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(strIP), port);

