namespace DiscordStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            int value = 0;

        Start:; // 레이블
            Console.Write("값을 입력하세요:");
            value = int.Parse(Console.ReadLine());

            if (value > 10)
            {
                goto Exit;  // 하향식 분기
            }

            Console.WriteLine($"{value} 값이 10보다 크거나 같아서 다시");

            goto Start; // 상향시 분기

            Console.WriteLine("절대 실행 안됨");

        Exit:;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("i=", +i);
            }

            Console.WriteLine($"{value}값이 10보다 작아서 탈출");
        }
    }
}