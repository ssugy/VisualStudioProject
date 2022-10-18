using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTest
{
    // 기본적인 콜백함수의 형태
    internal class 피호출자
    {
        public void Show()
        {
            Console.WriteLine("Show함수 실행"); //이게 일반적인 함수의 형태 - 단방향
        }

        public void Do(호출자 caller)
        {
            Console.WriteLine(caller.GetResult());  // 여기서의 호출이 콜백이라고 하고, GetResult()를 콜백함수라고 한다.
        }
    }

    class 호출자
    {
        public int GetResult()
        {
            return 10;
        }
        
        // 여기서 시작
        public void Test()
        {
            피호출자 target = new 피호출자();

            target.Show();  //일반적인 함수 형태
            target.Do(this);
        }
    }
}
