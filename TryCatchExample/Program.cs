namespace TryCatchExample
{
    internal class Program
    {
		/**
		 * 예외처리를 하고/안하고의 차이
		 * 1. 예외처리를 하지 않는다 : 예외가 발생하면, 그 문제를 바로 확인하겠다.
		 * 2. 예외처리를 하되, 파일에 기록한다. : catch에 기록된 내용 파일에 기록
		 */
        static void Main(string[] args)
        {
			int[] array = new int[4];
			try
			{
				Console.WriteLine("문자열을 입력하세요.");
				string userInput = Console.ReadLine();
				if (userInput.Contains("!"))
					throw new Exception("사용자입력에 !가 있습니다.");
				array[0] = 100;
				array[4] = 200;
				Console.WriteLine(array[4]);
				Console.WriteLine("try 구문 실행");	// 예외 발생해서 여기는 실행 안됨
			}
			//catch (IndexOutOfRangeException e)	// 배열 인덱스에 대한 예외
			catch (Exception e)	// 모든 예외에 대한 처리
			{
				Console.WriteLine("catch 구문 실행");
				Console.WriteLine(e.Message);
				return; // catch에서는 return을 일반적으로 작성하지 않는다. 기록의 용도, 예외 처리의 용도이다.
			}
			finally
			{	
				// 예외가 발생하지 않아도 finally는 실행된다.
				// finally 안에 있으면 return이 있어도 실행됨.
				// finally 밖에 있으면 return이 있으면 실행 안됨.
                Console.WriteLine("finally 구문 실행");	
			}
			Console.WriteLine("프로그램 종료");
        }
    }
}