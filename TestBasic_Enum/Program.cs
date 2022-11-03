namespace TestBasic_Enum
{
    internal class Program
    {

        static public int num1 = 0;
        static string str1 = "num1";
        public enum GameSTATE
        {
            act1 = 0,    // 0
            act2,        // 1
            act3,    // 5
            act4,       //2
            act5,       // 3
            act6,         // 4
            act7         // 5
        }

        static void Main(string[] args)
        {
            GameSTATE currentAct = GameSTATE.act1;
            switch (currentAct)
            {
                case GameSTATE.act1:
                    // 액트1일때 할 행동
                    break;
                case GameSTATE.act2:
                    break;
                case GameSTATE.act3:
                    break;
                case GameSTATE.act4:
                    break;
                case GameSTATE.act5:
                    break;
                case GameSTATE.act6:
                    break;
                case GameSTATE.act7:
                    break;
                default:
                    break;
            }

            //Console.WriteLine(TEST.num1);       // num1
            //Console.WriteLine((int)TEST.num3);       // 2
            //Console.WriteLine(str1 == TEST.num1.ToString());       // T
            //Console.WriteLine(str1.Equals(TEST.num1));       // F -> 서로다른 객체를 비교했기 때문.(타입이 달라서도 맞고, 객채 비교를 했기 때문)
            //Console.WriteLine(str1.Equals(TEST.num1.ToString()));       // T
            //Console.WriteLine(num1 == (int)TEST.num1);       // T
            //Console.WriteLine((short)TEST.num6);       // 10만 -> short라서 안됨 -> 

            //Console.WriteLine((int)GameSTATE.num1);
            //Console.WriteLine((int)GameSTATE.num2);
            //Console.WriteLine((int)GameSTATE.num3);
            //Console.WriteLine((int)GameSTATE.num4);
            //Console.WriteLine((int)GameSTATE.num5);
            //Console.WriteLine((int)GameSTATE.num6);
            //Console.WriteLine((int)GameSTATE.num7);


            intList.Add(1); // 주소가 0번
            intList.Add(2); // 주소가 1번
            intList.Add(3);
            intList.Add(4);
            intList.Add(5); // 주소가 4번

            // 반복문을 써야되는데요, for문, foreach문, while, do-while도 있지만
            //for (int i = 0; i < intArray.Length; i++)
            //for (int i = 0; i < intList.Count; i++)
            //{
            //    Console.WriteLine(intList[i]);
            //}

            //foreach (int item in intList)
            //{
            //    Console.WriteLine(item);
            //}

            User client_0 = new User();
            client_0.SetUserNum(1);
            
            User client_1 = new User();
            client_1.SetUserNum(2);
            
            User client_2 = new User();
            client_2.SetUserNum(3);

            userList.Add(client_0);
            userList.Add(client_1);
            userList.Add(client_2);

            //for (int i = 0; i < userList.Count; i++)
            //{
            //    Console.WriteLine(userList[i].GetUserNum());
            //}

            foreach (User item in userList)
            {
                Console.WriteLine(item.GetUserNum());
            }
        }

        static List<int> intList = new List<int>();
        static int[] intArray = new int[5] { 1,2,3,4,5};

        static List<User> userList = new List<User>();
    }
}