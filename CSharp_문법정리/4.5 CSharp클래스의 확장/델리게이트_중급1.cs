using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    public class Mathematics
    {
        delegate int CalcDelegate(int a, int b);

        static int Add(int x, int y) => x + y;
        static int Subtract(int x, int y) => x - y;
        static int Multiply(int x, int y) => x * y;
        static int Divide(int x, int y) => x / y;

        CalcDelegate[] methods = { Add, Subtract, Multiply, Divide};    // 델리게이트 배열로 넣기.

        //생성자 - static메서드를 가리키는 델리게이트 배열 초기화
        public Mathematics() 
        {
            //methods
        }

        // methods배열에 담긴 델리게이트를 opCode 인자에 따라 호출
        // 굳이 이걸..? 왜 굳이 이걸 델리게이트로 써야되지? 굳이? 
        public void Calculate(char opCode, int operand1, int operand2)
        {
            switch (opCode)
            {
                case '+':
                    Console.WriteLine($"+ : {methods[0](operand1, operand2)}");
                    break;
                case '-':
                    Console.WriteLine($"- : {methods[1](operand1, operand2)}");
                    break;
                case '*':
                    Console.WriteLine($"* : {methods[2](operand1, operand2)}");
                    break;
                case '/':
                    Console.WriteLine($"/ : {methods[3](operand1, operand2)}");
                    break;
                default:
                    break;
            }
        }
    }

    internal class 델리게이트_중급1
    {
        delegate void WorkDelegate(char a, int c, int d);

        static void Main()
        {
            Mathematics mathematics= new Mathematics();
            WorkDelegate work = mathematics.Calculate;

            work('*', 3, 20);
        }
    }
}
