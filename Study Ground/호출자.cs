using System;
using System.Collections.Generic;

namespace Study_Ground
{
    internal class 호출자
    {
        static void Main(string[] args)
        {
            /**
             * 딜리게이트  = 함수를 저장 할 수 있는 변수 선언.
             */
        }

        public void Start()
        {
            피호출자 target = new 피호출자();
            target.Do(this);    
        }

        public void GetResult()
        {
            Console.WriteLine("호출자의 GetResult실행");
        }

    }

    class 피호출자
    {
  
        public void Do(호출자 caller)
        {
            caller.GetResult();
        }
    }
}
