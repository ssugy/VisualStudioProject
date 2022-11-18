namespace _4._5_CSharp클래스의_확장
{
    public class HardDisk
    {
        // 중첩 클래스 형태
        // 중첩 클래스를 사용하기 위해서는 이렇게 public형태로 선언해줘야 외부에서 사용 가능하다.
        public class Platter
        {
            public Platter()
            {

            }
        }

        public class Head
        {
            //test233322ㅇㅇㅇ
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            HardDisk.Head head = new HardDisk.Head();  
        }
    }
}