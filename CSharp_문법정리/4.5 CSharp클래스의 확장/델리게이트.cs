using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    public class Disk
    {
        public int Clean(object arg)
        {
            Console.WriteLine("클린 작업 실행");
            return 0;
        }
    }

    // 델리게이트는 함수 포인터이다. 함수 자체를 가리킬 수 있다.
    internal class 델리게이트
    {
        delegate int Func(object obj);  // 함수 형태 지정
        static Func sample = null;

        static void _Main()
        {
            Disk disk = new Disk();
            sample = new Func(disk.Clean);
            sample = disk.Clean;    // c# 2.0부터 가능.
            Console.WriteLine(sample(5));
        }
    }
}
