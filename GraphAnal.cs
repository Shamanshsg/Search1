namespace Search1
{
    public class Graph
{
    public List<List<int>> list = new List<List<int>>();
    public int v;
    public int l = 0;
    public List<int> wereD = new List<int>();
    public List<int> wereDC = new List<int>();
    public List<int> wereB = new List<int>();
    public List<List<int>> turn = new List<List<int>>();
    public List<List<int>> comp = new List<List<int>>();
    private int w = 0;
    private bool w1 = true;
    public List<int> were = new List<int>();
    private bool c = true;
    public List<List<int>> edges = new List<List<int>>();
    private int p1 = 0;
    public List<List<int>> listCH = new List<List<int>>();
    public List<int> strongs = new List<int>();
    private int edge = 0;
    int time = 0;
    int col = 0;
    int z = 0;
    public List<List<int>> SCCMAS = new List<List<int>>();


    


    public Graph()
    {

    }

    public void BFS(int t)
    {
        for (int i = 0; i < list[t-1].Count; i++)
            {
                if(!turn[t-1].Contains(list[t-1][i]) && !wereB.Contains(list[t-1][i]))
                {
                turn[t-1].Add(list[t-1][i]);
                }
            }
         if (!wereB.Contains(t))
        {
            wereB.Add(t);
        }
        for (int i = 0; i < list[t-1].Count; i++)
            {
                if(!wereB.Contains(list[t-1][i]))
                {
                wereB.Add(list[t-1][i]);
                }
            }
        for (int i3 = 0; i3 < turn[t-1].Count; i3++)
        { 
            int p = turn[t-1][i3];
            turn[t-1].RemoveAt(i3);
            BFS(p);
        }


    }
    
    public void DFS(int t)
    {
        if (!wereD.Contains(t))
        {
            wereD.Add(t);
        }
        for (int i3 = 0; i3 < list[t-1].Count; i3++)
        {
            
            if (!wereD.Contains(list[t-1][i3]))
            {
                DFS(list[t-1][i3]);
            }


        }
    }

    public void DFScolor(int t,ref int[] color)
        {
            color[t-1] = 1;
            for (int i3 = 0; i3 < list[t-1].Count; i3++)
            {
                
                if (color[list[t-1][i3] - 1] == 0)
                {
                    DFScolor(list[t-1][i3], ref color);
                }
                else if (color[list[t-1][i3] - 1] == 1)
                {
                    c = false;
                }
            }
            color[t-1] = 2;
        }

    // 0-белый, 1-серый, 2-черный
    public void colorcycle(int t)
    {
        strong();
        if (strongs.Count == v)
        {
            connection(t);
            ed();
            if (component())
            {
                System.Console.WriteLine("Циикл есть");
                
            }
            else
            {
                System.Console.WriteLine("Цикла нет");
            }
        }
        else
        {
        int[] color = new int[v];
        for (int i = 1; i <= v; i++)
        {
            DFScolor(i,ref color);
        }
        if (c)
        {
            System.Console.WriteLine("Цикла нет");
        }
        if (!c)
        {
            System.Console.WriteLine("Цикл есть");
        }
        }
    }

    public void DFSancestor(int t, ref int[] entry, ref int[] exit)
    {
        entry[t-1] = time++;
        if (!wereD.Contains(t))
        {
            wereD.Add(t);
            w++;
        }
        for (int i3 = 0; i3 < list[t-1].Count; i3++)
        {
            
            if (!wereD.Contains(list[t-1][i3]))
            {
                DFSancestor(list[t-1][i3] ,ref entry ,ref exit);
            }


        }
        exit[t-1] = time++;
    }

    public void Sancestor(int start, int end)
    {
        int[] entry = new int[v];
        int[] exit = new int[v];
        DFSancestor(1,ref entry,ref exit);
        if (entry[start - 1] < entry[end - 1] && exit[start - 1] > exit[end - 1])
        {
            System.Console.WriteLine($"Вершина {start} является предком вершины {end}");
        }
        else
        {
            System.Console.WriteLine($"Вершина {start} не является предком вершины {end}");
        }
    }
    private bool check(int t)
    {
        for (int i = 0; i < listCH[t].Count; i++)
        {
            if (wereDC.Contains(listCH[t][i]))
            {
                return true;
            }
        }
        return false;
    }
    public void connection(int t)
    {
        change();
        //pin();
        w = 0;
        wereD.Clear();
        DFS(t);
        connect();
    }
    private void connect()
    {

        if (w == v && w1)
        {
            System.Console.WriteLine("Граф связан");
        }
        else
        {
            w1 = false;
            notconect();
        }
    }
    private void notconect()
    {
        pinFS(wereD);
        comp.Add(new List<int>());
        comp[z].AddRange(wereD);
        z++;
        were.AddRange(wereD);
        for (int i = 1; i <= v; i++)
        {
            if (!were.Contains(i))
            {
                wereD.Clear();
                DFS(i);
                notconect();
            }
        }
    }

//смена определенного на неопределеный
    public void change()
    {
        for (int i = 0; i < v; i++)
        {
            foreach (var i2 in list[i])
            {
                if (!list[i2-1].Contains(i+1))
                {
                    list[i2-1].Add(i+1);
                    list[i2-1].Sort();
                }
            }
        }
    }

    public void DFSC(int t)
    {
        if (!wereDC.Contains(t))
        {
            wereDC.Add(t);
            w++;
        }
        for (int i3 = 0; i3 < listCH[t-1].Count; i3++)
        {
            
            if (!wereDC.Contains(listCH[t-1][i3]))
            {
                DFSC(listCH[t-1][i3]);
            }
            else if (strongs.Contains(listCH[t-1][i3]) || check(listCH[t-1][i3]))
            {
                c = false;
            }


        }


    }
    private void cycle(int t)
    {
        DFS(t);
        DFSC(t);
        for (int i = 1; i <= v; i++)
        {
            if (!wereDC.Contains(i) && !wereD.Contains(i))
            {
                DFS(i);
                DFSC(i);
            }
        }
    }
    public void totalcycle(int t)
    {
        strong();
        change();
        copy();
        cycle(t);
        if (strongs.Count == v)
        {
            connection(t);
            ed();
            if (component())
            {
                System.Console.WriteLine("Циикл есть");
                
            }
            else
            {
                System.Console.WriteLine("Цикла нет");
            }
        }
        else
        {
            if (c)
        {
            System.Console.WriteLine("Цикла нет");
        }
        else
        {
            System.Console.WriteLine("Циикл есть");
        }
        }
        
    }
     
    private bool component()
    {
        for (int i = 0; i < comp.Count; i++)
        {
            col = 0;
            foreach (var i1 in comp[i])
            {
                col += list[i1-1].Count;   
            }
            col /= 2;
            if (col == comp[i].Count)
            {
                return true;
            }
        }
        return false;
    }
    private void strong ()
    {
        for (int i = 1; i <= v; i++)
        {
            foreach (var i2 in list[i-1])
            {
                if (list[i2-1].Contains(i))
                {
                    if (!strongs.Contains(i))
                    {
                        strongs.Add(i);
                    }
                    
                }
            }
        }
    }
    private void edg()
    {
        for (int i = 0; i < v*v; i++)
        {
            edges.Add(new List<int>());
            edges[i].Add(0);
            edges[i].Add(0);

        }
    }
    public void fill()
    {
        using (BinaryReader reader = new BinaryReader(File.Open("prv.txt", FileMode.Open)))
        {

            while (reader.PeekChar() > -1)
            {
                v = reader.ReadChar() - 48;
                edg();
                for (int i = 0; i < v; i++)
                {
                    turn.Add(new List<int>());
                }
                for (int i = 1; i <= v; i++)
                {
                    list.Add(new List<int>());
                    int y = reader.ReadChar() - 48;
                    for (int i1 = 0; i1 < y; i1++)
                    {
                        int p = reader.ReadChar() - 48;
                        list[i - 1].Add(p);
                        edges[p1][0] = i;
                        edges[p1][1] = list[i-1][i1];
                        p1++;
                    }
                }
            }
        }
    }
    public void pin(List<List<int>> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.Write("[");
            for (int i3 = 0; i3 < list[i].Count; i3++)
            {
                int i1 = list[i][i3];
                Console.Write(i1);
                Console.Write(", ");
            }
            Console.WriteLine("]");
        }
    }
    public void pinFS(List<int> were)
    {
        for (int i = 0; i < were.Count; i++)
        {
            int i2 = were[i];
            Console.Write($"{i2}, ");
        }
        System.Console.WriteLine(" ");
    }

    public void pinedge()
    {
        for (int i = 0; i < p1; i++)
        {
            System.Console.WriteLine($"({edges[i][0]}, {edges[i][1]} )");
        }
    }
// А шо?
    public void copy()
    {
        using (BinaryReader reader = new BinaryReader(File.Open("prv.txt", FileMode.Open)))
        {

            while (reader.PeekChar() > -1)
            {
                v = reader.ReadChar() - 48;

                for (int i = 1; i <= v; i++)
                {
                    listCH.Add(new List<int>());
                    int y = reader.ReadChar() - 48;
                    for (int i1 = 0; i1 < y; i1++)
                    {
                        int p = reader.ReadChar() - 48;
                        listCH[i - 1].Add(p);
                    }
                }
            }
        }
    }

//считают ребра
    private void ed()
    {
        for (int i = 0; i < p1-1; i++)
        {
            edge++;
            for (int i1 = i+1; i1 < p1; i1++)
            {
                if (edges[i][0] == edges[i1][1] && edges[i][1] == edges[i1][0])
                {
                    edge--;
                }
            }
        }
        edge++;
    }


    public void SCC()
    {
        List<int> exit = new List<int>(v);
        for (int i = 0; i < v; i++)
        {
            exit.Add(0);
        }
        for (int i = 1; i <= v; i++)
        {
            if (!wereD.Contains(i))
            {
                DFSSCC( i, exit);
            }
        }
        List<int> vertices = new List<int>();
        for (int i = 0; i < v*2; i++)
        {
            if (exit.Contains(i))
            {
                vertices.Add(exit.IndexOf(i) + 1);
            }
        }
        vertices.Reverse();
        for (int i = 0; i < v; i++)
        {
            listCH.Add(new List<int>());
        }
        infertile();
        List<int> push = new List<int>();
        int pushs = 0;
        int pop = 0;
        for (int i = 1; i <= v; i++)
        {
            if (!wereDC.Contains(i))
            {
                DFSSCCv2( i, vertices);
                SCCMAS.Add(new List<int>());
                push.AddRange(wereDC);
                push.RemoveRange(0, pushs);
                pushs = wereDC.Count;
                SCCMAS[pop].AddRange(push);
                pop++;
                push.Clear();
            }
        }
    }
    public void infertile()
    {
         for (int i = 0; i < v; i++)
        {
            foreach (var i2 in list[i])
            {
                listCH[i2-1].Add(i+1);
                listCH[i2-1].Sort();
            }
        }
    }

    private void DFSSCC( int t, List<int> exit)
    {
        time++;
        if (!wereD.Contains(t))
        {
            wereD.Add(t);
            w++;
        }
        for (int i3 = 0; i3 < list[t-1].Count; i3++)
        {
            
            if (!wereD.Contains(list[t-1][i3]))
            {
                DFSSCC(list[t-1][i3],exit);
            }


        }
        exit[t - 1] = time++;
    }

    public void DFSSCCv2(int t, List<int> vertices)
    {
        if (!wereDC.Contains(t))
        {
            wereDC.Add(t);
        }
        for (int i3 = 0; i3 < vertices.Count; i3++)
        {
            for (int i4 = 0; i4 < listCH[t-1].Count; i4++)
            {
                if (!wereDC.Contains(listCH[t-1][i4]) && listCH[t-1].Contains(vertices[i3]))
                {
                    DFSSCCv2(listCH[t-1][i4], vertices);
                }
            }
            


        }
    }

    public void topology()
    {
        List<int> exit = new List<int>(v);
        for (int i = 0; i < v; i++)
        {
            exit.Add(0);
        }
        for (int i = 1; i <= v; i++)
        {
            if (!wereD.Contains(i))
            {
                DFSSCC( i, exit);
            }
        }
        List<int> vertices = new List<int>();
        for (int i = 0; i < v*2; i++)
        {
            if (exit.Contains(i))
            {
                vertices.Add(exit.IndexOf(i) + 1);
            }
        }
        vertices.Reverse();
        pinFS(vertices);
    }

}
}