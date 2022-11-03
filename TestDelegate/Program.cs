namespace TestDelegate
{
    /**
     * 델리게이트 = 함수를 담을 수 있는 변수.
     * 원래 변수는 뭘담나요? => 값. 일반적인 값
     * ex) int num = 10; // 변수 num, 타입 : int, 값 : 10
     * 
     * 델리게이트 변수는 함수를 담을 수 있다. 이게 결정적으로 다릅니다.
     * 함수를 아무거나 막 담을 수 있나요?
     * 함수는 어떻게 이루어졌죠?
     * void TEST (int a, int b)  { } // 이거 통째로가 함수.
     * [1]  [2]  [3]             [4]
     * 
     * 1. 리턴타입
     * 2. 함수이름
     * 3. 매개변수(int a, int b)를 선언 할 수 있는 공간. 
     *  - 여기서 매개변수는 2개. 각각 매개변수 타입이 int
     * 4. 함수몸체
     * 
     * 이제 이 함수라는 것을 담는 변수. => 이게 델리게이트인데,
     * 델리게이트 변수 하나만 선언해서 모든 종류의 함수를 담을 수 있으면 얼마좋겠어요. 그런데 그렇게 못함.
     * 함수는 리턴타입, 매개변수의 갯수, 타입이 다를 수 있어요. 거기에 따라서 델리게이트도 다르게 선언해줘야되요.
     * (이 부분은 아래 코드로 보시면 이해가 더 쉽습니다.)
     */
    internal class Program
    {
        static void TEST() { Console.WriteLine("나은님 핸드폰 보세요?"); }  // 이 함수를 델리게이트형 변수에 하나에 담고 싶다.
        static void TEST2() { Console.WriteLine("용훈님 핸드폰 보세요?"); }  // 이 함수를 델리게이트형 변수에 하나에 담고 싶다.
        delegate void Do(); // 이제부터 Do라는 타입은, "void 함수이름()" 이런 함수들을 저장할 수 있는 타입입니다.

        static Do doCreate = TEST;  // 여기에는 함수가 들어간다.
        static int doCreate2 = 10;  //여기에 원래 값이 들어가는데,

        static void Main(string[] args)
        {
            doCreate += TEST2;

            doCreate();
            
            Console.WriteLine();
            
            doCreate -= TEST;
            doCreate();
            // 이런 부분들이 무슨 장점이 있냐하면, 특정 시점에 함수들을 등록 해놓고.(구독해놓고) 나중에 한꺼번에 실행 할 수 있어요.
        }

        
    }
}