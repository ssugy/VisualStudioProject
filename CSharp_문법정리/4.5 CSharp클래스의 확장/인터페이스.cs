using System;
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
        }
    }
}
