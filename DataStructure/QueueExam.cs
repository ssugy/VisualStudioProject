namespace DataStructure
{
    internal class QueueExam
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);

            Console.WriteLine(queue.Count);
            int queueCnt = queue.Count;
            Console.WriteLine();
            for (int i = 0; i < queueCnt; i++)  // 이거할때 조심해야 되겠네. queue.Count이걸로 하면 디큐할때마다 갯수가 줄어들어서 결국 반밖에 못보여줌
            {
                Console.WriteLine(queue.Dequeue());
            }
        }
    }
}