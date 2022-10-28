using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class StackExam
    {
        static void _Main()
        {
            /**
             * 스택 : LIFO ( Last In First Out - 나중에 들어온 데이터가 먼저 선택 됨)
             * 큐 : FIFO ( First In First Out - 처음에 들어온 데이터가 먼저 선택 됨)
             */
            Stack<int> stack = new Stack<int>();
            // 1. 스택에 데이터 추가 - Push
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            // 2. 스택에서 데이터 꺼내오기(삭제) - Pop
            int result = stack.Pop();
            Console.WriteLine(result);

            // 3. 스택에서 데이터 꺼내오기(삭제하지 않음) - Peak
            result = stack.Peek();
            Console.WriteLine(result);

            // 4. 스택을 배열로 변환
            int[] stackArr = stack.ToArray();
        }
    }
}
