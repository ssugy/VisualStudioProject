using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    class Computer
    {
        
    }

    interface IMonitor 
    { 
        void TurnOn();
        int Inch { get; set; }   
        int width { get;}
    }
    interface IKeyboard {}

    class NoteBook : Computer, IMonitor, IKeyboard
    {
        int inch;
        int width2;

        public int Inch { get => inch ; set => inch = value; }

        public int width => width2;    // 이름 겹치면 안된다.

        void IMonitor.TurnOn()  // 인터페이스를 이렇게 명시하면, 노트북 객체에서 접근은 불가능하다. 인터페이스로 형변환 해야된다.
        {
            Console.WriteLine("컴퓨터켜짐");
        }
    }

    internal class 인터페이스
    {
        static void Main()
        {
            NoteBook noteBook = new NoteBook();
            IMonitor moitor = noteBook as IMonitor;
            //noteBook.TurnOn();    // 명시적 인터페이스의 경우 이렇게는 안되고
            moitor.TurnOn();        //이런식으로 형변환 후 사용이 가능하다.

            // 인터페이스를 타입으로 받아서 오름차순이 아닌 내림차순으로 변경하는 예제(고급)
            int[] arr = new int[] { 1, 3, 2 };
            Array.Sort(arr, new IntegerCompare());  // 인터페이스 자체를 넣는게 아니라, 인터페이스를 상속받은 클래스를 넣는 것 이다.
                                                    // 어차피 그러면 부모객체 = new 자식객체()이런 개념으로 적용이 된다.(다형성)
            Console.WriteLine($"{arr[0]} {arr[1]} {arr[2]}");
        }
    }

    // 기본 오름차순이 아니라, 내림차순으로 변경하기 위해서 IComparer인터페이스를 상속받아서 오버라이딩함.
    class IntegerCompare : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null)
                return 0;   // 단순 null처리

            int xValue = (int)x;
            int yValue = (int)y;

            if (xValue > yValue)
                return -1;
            else if (xValue == yValue)
                return 0;

            return 1;
        }
    }
}
