using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5_CSharp클래스의_확장
{
    public delegate string GetResultDelegate();

    public class Target
    {
        public Target() { }
        public void Do(GetResultDelegate resultDelegate)
        {
            Console.WriteLine(resultDelegate());
        }
    }

    public class Source
    {
        public GetResultDelegate result = null;
        public Source() 
        {
            result = Result;
        }
        public string Result()
        {
            return "결과값";
        }
    }

    internal class 콜백메서드
    {
        static void _Main()
        {
            Source source= new Source();
            Target target= new Target();
            target.Do(source.result);
        }
    }
}
