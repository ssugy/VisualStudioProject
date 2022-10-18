using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dot5._0_CodingTestGround
{
    internal class Class1
    {
        int adress;

        //delegate용도는 함수를 담는 변수를 선언.
        delegate int CalcDelegate(int a, int b);

        void Main()
        {
            CalcDelegate calcDelegate = new CalcDelegate(ADD);  // 대리자를 이용해서 ADD함수를 담음.

            calcDelegate += Sub;
            calcDelegate = calcDelegate + Sub;

            calcDelegate -= ADD;
            calcDelegate(10, 5);
        }

        private int ADD(int a, int b)
        {
            Console.WriteLine($"ADD함수 실행 {a + b}");
            return a + b;
        }

        private int Sub(int a, int b)
        {
            Console.WriteLine($"ADD함수 실행 {a - b}");
            return a - b;
        }
    }
}
