

namespace ThreadExample
{
    internal class Program
    {
        static Thread t1;
        static void Main(string[] args)
        {
            // 이렇게 ThreadStart는 델리게이트 변수로, 스레드들을 모아서 한번에 실행 할 수 있다.
            ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            t1.Start();

            int count = 0;
            while (count < 1000)
            {
                count++;
                Console.WriteLine($"Main함수 {count}");
            }

            // 2. Join - 조인이면, t1 스레드가 멈출 때 까지 아래 용을 실행시키지 않는다.
            // 강사님 첨언 : 우선 Join을 하려면, sleep이 존재해야 한다. (잠들 때 조인함)
            // 여기서 join을 사용한 이유는, join-interrupt의 연속적인 사용이 스레드 종료되는 안전한 방법이라고 알고계심.
            t1.Join();
            Console.WriteLine("Join");

            // 3. Interrup - Thread를 안전하게 종료시키기 위한 코드.
            t1.Interrupt();
            Console.WriteLine("인터럽트 발생");   
        }

        private static void NewClient()
        {
            int threadCount = 0;
            while (threadCount < 300)
            {
                Console.WriteLine($"Thread Test {threadCount++}");
                Thread.Sleep(10);
            }
        }
    }
}