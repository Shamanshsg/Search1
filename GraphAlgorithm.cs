namespace Search1
{
    public class GraphAlgorithm : Graph
    {
        public List<int> fire = new List<int>();
        public List<List<int>> weights = new List<List<int>>();
        private List<bool> wereDej = new List<bool>();
        public List<int> road = new List<int>();
        public List<List<int>> roadMas = new List<List<int>>();
        public int[,] next;
        public int negga;
        public bool b = false;
        private bool[] used;

        int inf = 100000;
        public GraphAlgorithm()
        {
            fill();
            for (int i = 0; i < v; i++)
            {
                weights.Add(new List<int>());
                roadMas.Add(new List<int>());
                for (int i1 = 0; i1 < v; i1++)
                {
                    weights[i].Add(0);
                    if (i == i1){
                        roadMas[i].Add(0);
                    }
                    else
                    {
                        roadMas[i].Add(inf);
                    }
                }
            }
            int[,] lll = new int[v,v];
            next = lll;
            bool[] bbb = new bool[v];
            used = bbb;
            weight();
            System.Console.WriteLine("Граф");
            pin(list);
            System.Console.WriteLine("Вес");
            pin(weights);
            System.Console.WriteLine(" ");
            pin(roadMas);
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
                    roadMas[edges[i][0] - 1][edges[i][1] - 1] = push[i];
                    next[edges[i][0] - 1,edges[i][1] - 1] = edges[i][1];

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


        public void FordBellmana(int t)
        {
            for (int i = 0; i < v; i++)
            {
                road.Add(inf);
            }
            road[t - 1] = 0;
            for (int i = 0; i < v-1; i++)
            {
                for (int i1 = 0; i1 < edge; i1++)
                {
                    int u = edges[i1][0] - 1;
                    int u1 = edges[i1][1] - 1;
                    if (road[u1] > road[u] + weights[u][u1])
                    {
                        road[u1] = road[u] + weights[u][u1];
                    }
                }
            }
        }

        public void Floyd()
        {
            for (int i = 0; i < v; i++)
            {
                for (int i1 = 0; i1 < v; i1++)
                {
                    for (int i2 = 0; i2 < v; i2++)
                    {
                        if (roadMas[i1][i] + roadMas[i][i2] < roadMas[i1][i2])
                        {
                            roadMas[i1][i2] = roadMas[i1][i] + roadMas[i][i2];
                            next[i1,i2] = next[i1,i];
                        }
                        for (int i3 = 0; i3 < v; i3++)
                        {
                            if (roadMas[i3][i3] < 0)
                            {
                                System.Console.WriteLine("Есть отрицательный цикл");
                                b = true;
                                break;
                            }
                        }
                        if(b){break;}
                    }
                    if(b){break;}
                }
                if(b){break;}
            }
        }

        public void shortpath(int u, int u1)
        {
            if (roadMas[u - 1][u1 - 1] == 0)
            {
                System.Console.WriteLine("Пути нет");
            }
            int c = u;
            System.Console.WriteLine(" ");
            while (c != u1)
            {
                System.Console.Write($"{c} --> ");
                c = next[c - 1, u1 - 1];
            }
            System.Console.Write(u1);
        }

        public void pinMas(ref int[,] road)
        {
            for (int i = 0; i < v; i++)
        {
            Console.Write("[");
            for (int i3 = 0; i3 < v; i3++)
            {
                int i1 = road[i,i3];
                Console.Write(i1);
                Console.Write(", ");
            }
            Console.WriteLine("]");
        }
        }

        public void beauty()
        {
            for (int i = 0; i < v; i++)
            {
                for (int i1 = 0; i1 < v; i1++)
                {
                    if (roadMas[i][i1] > 10000)
                    {
                        roadMas[i][i1] = 0;
                    }
                }
            }
        }

        public bool negative()
        {
            for (int i = 0; i < v; i++)
            {
                if (roadMas[i][i] < 0)
                {
                    negga = i;
                    return true;
                }
            }
            return false;
        }

        public void negshortpath(int u, int u1)
        {
            bool b = true;
            int c = u - 1;
            System.Console.WriteLine(" ");
            while (c != u1)
            {
                if(b)
                {
                    c++;
                    b = false;
                }
                System.Console.Write($"{c} --> ");
                c = next[c - 1, u1 - 1];
            }
            System.Console.Write(u1);
        }

        public void Prim()
        {
            int sum = 0;
            int[] minedg = new int[v];
            int[] sel = new int[v];
            for (int i = 0; i < v; i++)
            {
                minedg[i] = inf;
                sel[i] = -1;
            }
            minedg[0] = 0;
            for (int i = 0; i < v; i++)
            {
                int ver = -1;
                for (int i1 = 0; i1 < v; i1++)
                {
                    if (!used[i1] && (ver == -1 || minedg[i1] < minedg[ver]))
                    {
                        ver = i1;
                    }
                }
                if (minedg[ver] == inf)
                {
                    System.Console.WriteLine("Noup");
                    
                }
                used[ver] = true;
                if(sel[ver] != -1)
                {
                    System.Console.WriteLine($"({ver + 1} , {sel[ver] + 1})");
                    sum += roadMas[ver][sel[ver]];
                }

                for (int i1 = 0; i1 < v; i1++)
                {
                    if(roadMas[ver][i1] < minedg[i1])
                    {
                        minedg[i1] = roadMas[ver][i1];
                        sel[i1] = ver;
                    }
                }
            }
            System.Console.WriteLine($"Вес = {sum}");
        }

    }
}
