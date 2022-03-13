namespace Search1
{
    public class GraphAlgorithm : Graph
    {
        private int po = 0;


        public GraphAlgorithm()
    {

    }
        public void BFSv2(int t)
        {
            fill();
            pin(list);
            List<int> queue = new List<int>();
            wereB.Add(t);
            queue.Add(t);
            while(queue.Count != 0)
            {
            int cur = queue[0];
            queue.RemoveAt(0);
            for (int i = 0; i < list[cur - 1].Count; i++)
            {
                System.Console.WriteLine(po);
                if (!wereB.Contains(list[cur - 1][i]))
                {
                    wereB.Add(list[cur - 1][i]);
                    queue.Add(list[cur - 1][i]);   
                }
            }
            }
        }
        
    }
}