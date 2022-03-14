namespace Search1
{
    public class GraphAlgorithm : Graph
    {
        public List<int> fire = new List<int>();
        public List<List<int>> weights = new List<List<int>>();
        private List<bool> wereDej = new List<bool>();
        public List<int> road = new List<int>();
        public GraphAlgorithm()
        {
            fill();
            for (int i = 0; i < v; i++)
            {
                weights.Add(new List<int>());
                for (int i1 = 0; i1 < v; i1++)
                {
                    weights[i].Add(0);
                }
            }
            weight();
            pin(list);
            pin(weights);
        }
        public void BFSv2(int t, int end)
        {
            List<int> push = new List<int>();
            List<int> queue = new List<int>();
            wereB.Add(t);
            queue.Add(t);
            while(queue.Count != 0)
            {
            if (queue.Contains(end)){
                break;
            }
            int cur = queue[0];
            queue.RemoveAt(0);
            push.Add(list[cur - 1].Count);
            for (int i = 0; i < list[cur - 1].Count; i++)
            {
                if (!wereB.Contains(list[cur - 1][i]))
                {
                    wereB.Add(list[cur - 1][i]);
                    queue.Add(list[cur - 1][i]);
                    fire[list[cur - 1][i] - 1] = fire[cur - 1] +1;
                }

            }
            }
        }
        
        public void away (int start, int end)
        {
            for (int i = 0; i < v; i++)
            {
                fire.Add(0);
            }
            BFSv2(start, end);
            System.Console.WriteLine($" Длинна пути от вершины {start} до вершины {end} = {fire[end -1]}");
        }

        public void weight()
        {
            List<int> push = new List<int>();
            ed();
            if (File.Exists("weight.txt"))
            {
                string content = File.ReadAllText("weight.txt");
                int[] a = content.Split(' ').Select(x => int.Parse(x)).ToArray();
                push.AddRange(a);
                for (int i = 0; i < push.Count; i++)
                {
                    weights[edges[i][0] - 1][edges[i][1] - 1] = push[i];

                }
            };
            
        }

        public void Dijkstra(int t)
        {
            for (int i = 0; i < v; i++)
            {
                wereDej.Add(false);
                road.Add(10000000);
            }
            road[t - 1] = 0;
            for (int i = 0; i < v; i++)
            {
                int l = -1;
                for (int i1 = 0; i1 < v; i1++)
                {
                    if (!wereDej[i1])
                    {
                        if (l == -1 || road[i1] < road[l])
                        {
                            l = i1;
                        }
                        
                    }
                }
                System.Console.WriteLine($"i = {i}, l = {l}");
                 if (road[l] == 10000000)
                {
                      break;
                }
                wereDej[l] = true;
                for (int i3 = 0; i3 < list[l].Count; i3++)
                {
                    if (road[l] + weights[l][list[l][i3] - 1] <  road[list[l][i3] - 1])
                    {
                        road[list[l][i3] - 1  ] = road[l] + weights[l][list[l][i3] - 1];
                    }
                }
            }
        }
    }
}