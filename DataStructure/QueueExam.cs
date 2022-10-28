namespace DataStructure
{
    /// <summary>
    /// 큐를 직접 만들어보기
    /// 1. 리스트로 만들어야 겠다.
    /// </summary>
    class PersonalQueue <T>
    {
        List<T> queueList;
        int startPos;
        int endPos;

        public PersonalQueue()
        {
            queueList = new List<T>();
            startPos = 0;
            endPos = 0;
        }

        // 값을 리스트에 저장
        public void Enqueue(T num)
        {
            queueList.Add(num);
            endPos++;
        }

        public T Dequeue()
        {
            T t = queueList[endPos];
            queueList.RemoveAt(endPos);
            return t;
        }
    }

    internal class QueueExam
    {
        static void _Main(string[] args)
        {
            PersonalQueue<int> test = new PersonalQueue<int>();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);

            Console.WriteLine($"Peak(데이터 삭제하지 않고 가져오기 : {queue.Peek()}\n");    // peek는 데이터를 삭제하지 않고 가져오는 것

            Console.WriteLine($"큐 길이 : {queue.Count}\n");
            int queueCnt = queue.Count;
            for (int i = 0; i < queueCnt; i++)  // 이거할때 조심해야 되겠네. queue.Count이걸로 하면 디큐할때마다 갯수가 줄어들어서 결국 반밖에 못보여줌
            {
                Console.WriteLine(queue.Dequeue());
            }

            //큐의 데이터를 배열로 변환하기
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            int[] convertQueue = queue.ToArray();
            Console.WriteLine(convertQueue[0]);
            Console.WriteLine(queue.Peek());
        }
    }
}