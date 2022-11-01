using System.Diagnostics;

namespace Chatting_1_ProcessChecker
{
    internal class ProcessChecker
    {
        static void Main(string[] args)
        {
            // 실행파일 경로와 이름
            string clientExePath = @"D:\Work\CSharp\VisualStudio Projects\Chatting_1_Client\bin\\Debug\" + "Chatting_1_Client.exe";
            string serverExePath = @"D:\Work\CSharp\VisualStudio Projects\Chatting_1_Server\bin\Debug\" + "Chatting_1_Server.exe";
            
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = true;
            processStartInfo.CreateNoWindow = false;    // 새로운 창 띄우기
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            processStartInfo.FileName = clientExePath;


            //클라이언트 구동 - 창을 서로 다르게 해야된다.
            for (int i = 0; i < 200; i++)
            {
                Process.Start(processStartInfo);
            }
        }
    }
}