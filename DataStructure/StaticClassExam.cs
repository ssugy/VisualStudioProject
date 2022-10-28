using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class StaticClassExam
    {
        static int num2 = 10;
        static void Main()
        {
            num2 = 11;
            Console.WriteLine("dddd");
            
            StaticClassExam staticClassExam = new StaticClassExam();
            staticClassExam.TEST();
        }

        /**
         * 질문 : 변수와 함수를 모두 static으로 했는데 클래스 자체를 static으로 안한 이유가 있나요?
         * 강사님 답변
         *  - 메인함수가 스태틱이기 때문에 스태틱 변수로 선언해야 된다. (세모) -> 선언은 마음대로 해도된다?(스태틱 클래스가 아니기 때문) 
         *                                                                     -> 메인함수에서 사용하려면 스태틱으로 선언해야된다.
         *   -- 스태틱으로 선언안하면 못쓰냐? -> 쓸수있다.
         *  - 메인함수가 스태틱이기 때문에 스태틱 함수로 선언해야 한다. (세모)
         *  - 함수와, 변수가 모두 스태틱이기 때문에 스태틱 클래스와 같다 => 틀린이야기
         */

         public int num = 10;
        public void TEST()
        {
            Console.WriteLine("해장국");
        }

    }
}
