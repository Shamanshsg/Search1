namespace Search1
{
    public class DSU : GraphAlgorithm
    {
        int[] parent;
        Random rand = new Random();
        public DSU()
        {
            int[] p = new int[v];
            parent = p;
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
    }
}