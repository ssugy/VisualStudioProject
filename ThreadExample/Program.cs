

namespace ThreadExample
{
    internal class Program
    {

        static void Main(string[] args)
        {
			try
			{
				// 1. 에러 발생 -> catch -> 크리티컬 한게 아니면 그대로 쭉 실행 시킬 수 있다.
				Console.WriteLine("트라이문 실행");
                int[] arr = new int[10];
                int b = arr[0] / 0;
            }
			catch (NullReferenceException e)
			{
                // 2. 어떤 에러가 발생할 때 넣어줄까???
                Console.WriteLine($"1 캐치 실행 {e.Message}");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"2 캐치 실행 {e.Message}");
            }
			finally
			{
                // 무.조.건. 실행 된다.
                Console.WriteLine("파이널리 실행");
            }

            Console.WriteLine("트라이문 뒤에 실행 할 일 들");
        }
    }
}