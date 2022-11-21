using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    public delegate bool CompareAscDesc(int a, int b);
    // 선택 정렬 정의
    class SortObject
    {
        int[] numbers;

        public SortObject(int[] numbers)
        {
            this.numbers = numbers;
        }

        // 전형적인 선택 정렬 알고리즘 구현
        // 배열을 크기순으로 정렬
        public void Sort(CompareAscDesc compare)
        {
            int temp;
            for (int i = 0; i < numbers.Length; i++)
            {
                int lowPos = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (compare(numbers[lowPos], numbers[j]))    // 단순히 여기의 < 이거냐, > 이거냐에 따라서 오름차순/내림차순으로 갈린다. 이 부분을 델리게이트로 전환
                    {
                        lowPos = j;
                    }
                }

                // lowPos안에는 가장 적은 숫자가 담긴 배열번호가 저장된다. - 그래서i번째와 변경
                temp = numbers[lowPos];
                numbers[lowPos] = numbers[i];
                numbers[i] = temp;
            }
        }

        public void Display()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + ", ");
            }
        }
    }

    internal class 델리게이트_선택정렬
    {
        static void _Main()
        {
            int[] intArray = new int[] { 5, 2 , 3, 1, 7};

            SortObject so = new SortObject(intArray);
            // 여기서 델리게이트가 필요한 이유는 오름차순과 내림차순이 단순히 <, >이 기호 하나 차이로 변하기 때문에 델리게이트로 만들어서 진행
            // 여기서 추가적으로 오브젝트 형태로 변환해서 델리게이트를 만든뒤에 진행하는데, 그런 방법도 가능하다.
            so.Sort(CompareASC);
            so.Display();

            Console.WriteLine();
            so.Sort(CompareDESC);
            so.Display();

        }

        private static bool CompareASC(int a, int b)
        {
            return a < b;
        }

        private static bool CompareDESC(int a, int b)
        {
            return a > b;
        }
    }
}
