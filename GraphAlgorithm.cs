namespace Search1
{
    public class GraphAlgorithm : Graph
    {
        public List<int> fire = new List<int>();
        public List<List<int>> weights = new List<List<int>>();
        private List<bool> wereDej = new List<bool>();
        public List<int> road = new List<int>();
        public List<List<int>> roadMas = new List<List<int>>();
        public List<List<int>> res = new List<List<int>>();
        public int[,] next;
        public int negga;
        public bool b = false;
        private bool[] used;
        public int[,] g;
        public int[] parent;
        public int sum = 0;
        private int[,] degrees;

        public int[] path;
        Random rand = new Random();

        int inf = 100000;
        public GraphAlgorithm()
        {
            fill();
            int[] p = new int[v];
            parent = p;
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
            ed();
            degrees = new int[v,2];
            g = new int[edge,3];
            next = new int[v,v];
            used = new bool[v];
            path = new int[v];
          
            weight();
            System.Console.WriteLine("Граф");
            pin(list);
            System.Console.WriteLine("Вес");
            pin(weights);
            System.Console.WriteLine(" ");
            //for (int i = 0; i < v; i++)
            // {
            //     for (int i1 = 0; i1 < v; i1++)
            //     {
            //         System.Console.Write(next[i,i1]);
            //     }
            //     System.Console.WriteLine();
            // }
            fillg();
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
            //pin(edges);
            List<int> push = new List<int>();
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

        private void fillg()
        {
            System.Console.WriteLine(edge);
            int i1 = 0;
            for (int i = 0; i < v - 1; i++)
            {
                int lol = 0;
                lol += i + 1;
                while (lol < v )
                {
                    if (roadMas[i][lol] != inf)
                    {
                        //System.Console.WriteLine($"i = {i} lol = {lol}  i1 = {i1}");
                        g[i1,0] = roadMas[i][lol];
                        g[i1,1] = i + 1;
                        g[i1,2] = lol + 1;
                        i1++;
                    }
                    lol++;
                }
            }
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
                    break;
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


        public void Kruskala()
        {
            int pop = 0;
            // g[i,i1]   i=номер вершины, i1 = 1(Вес), i1 = 2,3(Начальная и конечная вершина)
            int cost = 0;
            SortColumn(ref g);
            for (int i = 0; i < v; i++)
            {
                parent[i] = i;
            }
            for (int i = 0; i < edge; i++)
            {
                int a = g[i,1];
                int b = g[i,2];
                int l = g[i,0];
                if(Find(a - 1) != Find(b - 1))
                {
                    cost += l;
                    res.Add(new List<int>());
                    res[pop].Add(g[i,1]);
                    res[pop].Add(g[i,2]);
                    sum += g[i,0]; 
                    pop++;
                    Unite(a - 1,b - 1);
                }
            }
        }

        public void Sort()
        {
            fillg();
            SortColumn(ref g);
            for (int i = 0; i < g.GetLength(0); i++, Console.WriteLine())
                for (int j = 0; j < g.GetLength(1); j++)
                    Console.Write(g[i, j] + "\t");
 
 
            Console.ReadKey();
        }   

        void SortColumn(ref int[,] matr,uint column=0)
        {
            if (matr == null || column > matr.GetLength(1))
                throw new ArgumentException();
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(0)-1; j++)
                    if(matr[j,column]>matr[j+1,column])
                    {
                        for (int c= 0; c < matr.GetLength(1); c++)
                        {
                            var temp = matr[j, c];
                            matr[j, c] = matr[j + 1, c];
                            matr[j + 1, c] = temp;
                        }
                    }
        }

        public void Makeset(int x)
        {
            parent[x] = x;
        }

        public int Find(int x)
        {
            if (parent[x] == x){return x;}
            return parent[x] = Find(parent[x]);
        }

        public void Unite(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            if (rand.Next() % 2 == 0)
                Swap(ref x, ref y);
            parent[x] = y;
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        // 0 = входящщяя  1 - выходящщяя
        public void degree()
        {
            int outgo = 0;
            int incom = 0;
            for (int i = 1; i <= v; i++)
            {
                outgo = 0;
                incom = 0;
                for (int i1 = 1; i1 <= v; i1++)
                {
                    if (weights[i-1][i1-1] != 0)
                    {
                        incom++;
                    }
                    if (weights[i1-1][i-1] != 1)
                    {
                        outgo++;
                    }
                }
            degrees[i - 1,0] = incom;
            degrees[i - 1,1] = outgo;
            }
            // for (int i = 0; i < v; i++)
            // {
            //     System.Console.WriteLine(" ");
            //     System.Console.Write($"{degrees[i, 0]}  {degrees[i, 0]}");
            // }
        }
        public bool Eulercheck()
        {
            int OddV = 0;
            for (int i = 0; i < v; i++)
            {
                if(degrees[i,0]%2 == 1)
                {
                    OddV++;
                }
            }
            if(OddV > 2)
            {
                return false;
            }
            for (int i = 0; i < v; i++)
            {
                if (degrees[i,0] > 0)
                {
                    DFS(i + 1);
                    break;
                }
            }
            for (int i = 0; i < v; i++)
            {
                if (degrees[i,0]  > 0 && !wereD.Contains(i+1))
                {
                    return false;
                }
            }
            return true;
        }

        public List<int> Euler(int t)
        {
            List<List<int>> temp = new List<List<int>>(weights);
            List<int> stack = new List<int>();
            stack.Add(t);
            int i;
            int cur;
            List<int> result = new List<int>();
            while (stack.Count != 0)
            {
                cur = stack.Last();
                i = 0;
                bool nvite = false;
                for (i = 0; i < v; i++)
                {
                    if (temp[cur][i] != 0)
                    {
                        nvite = true;
                        break;
                    }
                }
                if (i == v && !nvite)
                {
                    result.Add(cur + 1);
                    stack.RemoveAt(stack.Count - 1);
                } else {
                    temp[cur][i] = 0;
                    temp[i][cur] = 0;
                    stack.Add(i);
                }
            }
            return result;
        }
    
        private bool Path(int t, int p)
        {
            if (weights[path[p-1]][t] == 0)
            {
                return false;
            }
           for (int i = 0; i < p; i++)
           {
               if (path[i] == t)
               {
                   return false;
               }
           }
            return true;
        }
        private bool hamilton(int n){
                if (n == v)
                {
                    if (weights[path[n - 1]][path[0]] != 0)
                    {
                        return true;
                    }
                    else{
                        return false;
                    }
                }
                for (int i = 0; i < v; i++)
                {
                    if (Path(i, n))
                    {
                        path[n] = i;
                        if (hamilton(n + 1))
                        {
                            return true;
                        }
                        path[n] = -1;
                    }
                }

            return false;
        }

        public void cycle(){
            for (int i = 0; i < v; i++)
            {
                path[i] = -1;
            }
            path[0] = 0;
            if (!hamilton(1))
            {
                System.Console.WriteLine("No");
            } else {
                printHam();
            }
        }

        private void printHam(){
             for (int i = 0; i < v; i++){
                Console.Write(" " + (path[i] + 1) + " ");
             }
            Console.WriteLine(" " + (path[0] + 1) + " ");
        }
    
    
    }
}

