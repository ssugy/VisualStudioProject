using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    class Point
    {
        int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"X : {x}, Y : {y}";
        }
    }

    /**
     * 추상클래스, new를 사용못함.
     * 함수 몸체 추상 메서드만 보유 가능
     */
    abstract class DrawingObject
    {
        public int a = 10;  // 이런 단순 변수는 적용 가능함.
        public abstract void Draw();
        public void Move() => Console.WriteLine("Move");
        
    }

    class Line : DrawingObject
    {
        Point pt1, pt2;
        public Line(Point pt1, Point pt2)
        {
            this.pt1 = pt1;
            this.pt2 = pt2;
        }

        public override void Draw()
        {
            Console.WriteLine($"Line {pt1.ToString()} ~ {pt2.ToString()}");
        }
    }

    internal class 추상클래스
    {
        static void _Main()
        {
            DrawingObject line = new Line(new Point(10, 10), new Point(20, 20));
            line.Draw();    // 다형성에 따라 Line.Draw호출됨.    - 굳이 다형성으로 이렇게 작성할 이유가있나
            Console.WriteLine(line.a);
        }
    }
}
