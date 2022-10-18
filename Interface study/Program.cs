using System;

namespace Interface_study
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1 여기서 인터페이스의 사용이 나온다. 다형성을 이용한다.
            IDrawObject[] instance = new IDrawObject[] { new Line(), new Rectangle(), new Circle()};

            foreach (var item in instance)
            {
                item.Draw();
            }

            부모클래스 test = new Line();    //이렇게 생성하면 인터페이스가 포함이 안된다.
            test.Draw();    
        }
    }

    /**
     * 1. 인터페이스에 속한 메서드는 가상 메서드로 간주한다. 그래서 Virtual을 쓰지 못하게 막고있다. 언제나 virtual이기 때문
     */
    interface IDrawObject
    {
        void Draw();
    }

    class 부모클래스
    {
        public virtual void Draw2()
        {
            Console.WriteLine("부모 클래스의 Draw");
        }
    }

    // 와나... 대박 Draw삭제해도 진행 가능함 이렇게 가능하네.
    class Line : 부모클래스, IDrawObject
    {
        //public override void Draw()
        //{
        //    Console.WriteLine("라인 그리기");
        //}

        public void Draw()
        {
            Console.WriteLine("자식 클래스의 Draw");
        }

        public override void Draw2()
        {
            Console.WriteLine("자식 클래스의 Draw");
        }
    }

    class Rectangle : IDrawObject
    {
        public void Draw()
        {
            Console.WriteLine("사각형 그리기");
        }
    }

    class Circle : IDrawObject
    {
        public void Draw()
        {
            Console.WriteLine("원 그리기");
        }
    }
}
