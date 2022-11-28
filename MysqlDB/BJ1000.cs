using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlDB
{
    internal class BJ1000
    {
        // 두 정수 a와 b를 입력받은 다음 두수의 합을 출력 
         public static void _Main()
        {
            while (true)
            {
                var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
                var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
                string? num = sr.ReadLine();
                int n1 = int.Parse(num);
                
                if (n1 == 0)
                    break;
                
                n1 = n1 * 3;
                int n2, n3, n4;
                if (n1 % 2 == 0)
                {
                    n2 = n1 / 2;
                }
                else
                {
                    n2 = (n1 + 1) / 2;
                }
                n3 = n2 * 3;
                n4 = n3 / 9;

                switch (n4 % 2)
                {
                    case 0:
                        sw.WriteLine("even " + n4);
                        break;
                    case 1:
                        sw.WriteLine("odd " + n4);
                        break;
                }
                sr.Close();
                sw.Close();
            }
        }
    }
}
