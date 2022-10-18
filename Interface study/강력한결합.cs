using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_study
{
    internal class 강력한결합
    {
        public void PowerOn(Computer com)
        {
            com.TurnOn();   //강력한 결합의 예 -> 여기서 모니터 연결한다고 하면 모니터 클래스 생성하고, 여기도 바꿔야한다.
            // 단지 컴퓨터에서 모니터로 바꿨는데, 여기의 파라미터 타입을 바꾸는게 당연한 것인가? 아니다라는 의미. 그래서 느슨한 결합이 나옴.
            // 파라미터를 인터페이스를 받는 형식으로 변경함.
        }
    }

    class Computer
    {
        public void TurnOn()
        {
            Console.WriteLine("컴퓨터 켜기");
        }
    }

    //class Monitor
    //{
    //    public void TurnOn()
    //    {
    //        Console.WriteLine("모니터 켜기");
    //    }
    //}
}
