namespace CallBack_Sample
{
    delegate int GetResultDelegate();

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    class Target
    {
        public void Do(GetResultDelegate getResult)
        {
            Console.WriteLine(getResult()); // 콜백 메서드 호출
        }
    }

    class Source
    {
        public int GetResult()
        {
            return 10;
        }
        
        public void Test()
        {
            Target target = new Target();
            target.Do(new GetResultDelegate(this.GetResult));
        }
    }
}