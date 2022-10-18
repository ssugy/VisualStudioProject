using System;

namespace DelegateTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();

            //ShowMenu_End(GetAge_Korea); // 최종 사용 방법
        }

        // 1 : 사용자의 나이에 맞춰서 메뉴를 출력한다.
        public static void ShowMenu()
        {
            int age = GetAge_Korea();

            if (age >= 20)
            {
                // 20살 이상의 메뉴 출럭
            }
            else
            {
                // 20살 미만의 메뉴 출력
            }
        }

        // 1-1 : 한국인 나이 기준으로 메뉴를 출력한다.
        public static int GetAge_Korea()
        {
            // DB에서 현재 고객의 생년을 가져온다. (2004)
            // 현재 일시(2022) - 생년(2004)을 빼준뒤에(18), 한국은 +1한다(최종 19세). (0살의 개념이 없음)
            
            return 0;
        }

        // 1-2 : 일본에 출시하려고 보니까 나이개념이 다르다. 날 때부터 0살로 친다.
        public static int GetAge_Japan()
        {
            // DB에서 현재 고객의 생년을 가져온다. (2004)
            // 현재 일시(2022) - 생년(2004)을 빼준뒤에(18)(최종 18세). (0살의 개념이 있음)

            return 0;
        }

        /**
         * 1-3 이런식으로 함수를 구성하게 되면, 딱봐도 문제가 있습니다.
         * 
         * 1. 이게 나이계산의 방법이 늘어날수록, ShowMenu의 함수의 종류를 늘리게 됩니다.
         * 
         * 2. 만약 태그같은걸로 if("JAPAN")이런식으로 구분한다고 해도, 그 자체가 함수 종류가 늘어날 때 마다 
         * ShowMenu자체를 계속 바꿔야되는 것 자체가 문제입니다.
         *  - 이 부분이 전형적으로 OOP의 2번째 원칙인 open-close-principle 개방-폐쇄 원칙 위반이라고 합니다.
         *  
         *  oop는 5개의 원칙이 존재하며 SOLID원칙이라고 합니다.
         *  
         *  이걸 개선하기 위해서는 int age = GetAge()여기에서 GetAge()부분을 파라미터로 빼낼 수 있어야 합니다.
         *  그 방법을 실현하기 위해서는 Delegate사용을 고려해봐야 합니다.
         */
        //public static void ShowMenu_Japan()
        //{
        //    int age = GetAge_Japan();

        //    if (age >= 20)
        //    {
        //        // 20살 이상의 메뉴 출럭
        //    }
        //    else
        //    {
        //        // 20살 미만의 메뉴 출력
        //    }
        //}

        //최종본
        public delegate int MyDelegate();

        public static void ShowMenu_End(MyDelegate GetAge)
        {
            int age = GetAge();

            if (age >= 20)
            {
                // 20살 이상의 메뉴 출럭
            }
            else
            {
                // 20살 미만의 메뉴 출력
            }
        }
    }
}
