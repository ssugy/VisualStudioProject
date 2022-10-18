using System;
using System.Net;
using System.Net.Sockets;

namespace GameNetwork_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 자신의 아이피 아는 방법
            // 1. 호스트 / 도메인명에서 IP알아내기
            IPHostEntry hostEntry = Dns.GetHostEntry("www.naver.com");
            IPHostEntry hostEntry_google = Dns.GetHostEntry("www.google.com");

            Console.WriteLine(hostEntry_google.HostName);
            foreach (IPAddress item in hostEntry_google.AddressList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            // 2. 로컬 호스트명 정보 얻기
            string hostName = Dns.GetHostName();
            Console.WriteLine("호스트명(인터넷상에서의 내 PC이름) :" + hostName);
            IPHostEntry localHost = Dns.GetHostEntry(hostName);
            foreach (IPAddress item in localHost.AddressList)
            {
                Console.WriteLine("로컬 IP : " + item);
            }

            Console.WriteLine();
            // 3. 윈도우 명령 프롬프트 -> ipconfig를 통해서 확인 가능
            // ip를 문자열로 표현
            string IP = "172.30.1.16";
            IPAddress ip1 = IPAddress.Parse(IP);
            Console.WriteLine("1. ip를 문자열로 표현 : " + ip1);

            // 4. ip를 byte배열로 표현 - 생성자로 바로 변환
            byte[] byIP = new byte[] { 172, 30, 1, 16 };
            IPAddress ip2 = new IPAddress(byIP);
            Console.WriteLine("4. ip를 byte 배열로 표현 :" + ip2);

            // 5. 바이트 배열을 long값으로 표현
            uint IAddress = BitConverter.ToUInt32(byIP, 0);
            Console.WriteLine("ip를 long값으로 표현 : " + IAddress);
            IPAddress ip3 = new IPAddress((long)IAddress);
            Console.WriteLine("5. 바이트배열을 Long으로 변환 후 다시 IP : " + ip3);

            // 6. 바이트로 분리
            byte[] b3 = ip3.GetAddressBytes();
            string strIP = string.Format("{0}.{1}.{2}.{3}", b3[0], b3[1], b3[2], b3[3]);
            Console.WriteLine("6. 바이트분리(추출한IP) : " + strIP);

            // 7. ipv4를 ipv6로 변환
            IPAddress ipv6 = ip3.MapToIPv6();
            Console.WriteLine("7. ipv6로 변환 : " + ipv6);
        }
    }
}