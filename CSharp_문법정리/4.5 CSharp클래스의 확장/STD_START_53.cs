using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    /*
   인터페이스 : 구현 없이 메서드 선언만 포함
                메서드 선언을 0개 이상 포함할 수 있다.
                인터페이스 이름에는 I 접두사를 붙인다
                추상메서드만 0개 이상 담고 있는 추상클래스

  추상클래스로 구현할 수 있는 것을 왜 굳이 interface 라는 새로운 예약어를 만들어 표현할까?
  => 클래스는 다중상속이 불가능하다 라는 특징으로 설명할 수 있다.
  => 추상클래스는 다중상속을 할 수 있지만, 인터페이스는 클래스가 아니기 때문에 다중상속이 허용된다
   */

    abstract class Weekend  //추상클래스
    {
        public abstract void Saturday();
        public abstract void Sunday(int year);
    }

    interface IWeekend  //인터페이스
    {
        void Saturday();
        void Sunday(int year);
    }
    //------------------------------------------------------------------

    abstract class Product
    {
        public abstract void TurnOn();
    }

    interface ILaptop   //메서드 시그니처만을 포함하고 있는 인터페이스
    {
        void TurnOn();
    }

    interface INetBook  //비어있는 인터페이스 정의 가능
    {
        void TurnOn();
    }

    class LapTop : Product, ILaptop, INetBook   //클래스 상속과 함꼐 인터페이스로부터 다중상속 가능
    {
        public override void TurnOn()
        {
        }

        //public void TurnOn() { Console.WriteLine("1: Turn On"); }
        //추상메서드와는 달리 override 예약어가 필요 없음
        //인터페이스의 메서드를 자식 클래스에서 구현 할 때에는 반드시 public 제한자를 명시해야 한다
        //또는 void ILaptop.TurnOn(){} 처럼 인터페이스 명을 직접 붙인다


        //override public void TurnOn() { }

        void ILaptop.TurnOn() { }
        void INetBook.TurnOn() { }

        //void ILaptop.TurnOn() { Console.WriteLine("2: Turn On"); }
    }


    internal class STD_START_53
    {
        static void _Main(string[] args)
        {
            //1번 방법으로 호출
            LapTop lapTop = new LapTop();
            //lapTop.TurnOn();

            //2번 방법으로 호출 : 명시적으로 인터페이스의 멤버에 종속시킨다고 표시하는 것과 같다.
            //따라서 인터페이스로 형변환해야만 호출할 수 있다

            //LapTop lapTop1 = new LapTop();  //오류발생
            //lapTop1.TurnOn();


            ILaptop iLaptop = lapTop as ILaptop;
            iLaptop.TurnOn();   //반드시 인터페이스로 형변환해서 호출
        }
    }
}
