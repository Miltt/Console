using System;

namespace Set
{
    public class DisjointSet
    {
        private int[] _parent;
        private int[] _rank;

        public void Make(int a)
        {
            _parent = new int[a];
            _rank = new int[a];

            _parent[a] = a;
            _rank[a] = 0;
        }

        public int Find(int a)
        {
            if (_parent[a] == a)
                return a;

            return _parent[a] = Find(_parent[a]);
        }

        public void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);

            if (a != b)
            {
                if (_rank[a] < _rank[b])
                    _parent[a] = b;
                else
                    _parent[b] = a;

                if (_rank[a] == _rank[b])
                    _rank[a]++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var set = new DisjointSet();
            set.Make(5);
            set.Make(3);
            set.Make(1);
            set.Make(7);
            set.Make(4);

            set.Union(5, 3);
            set.Union(2, 1);
            set.Union(3, 4);

            Console.WriteLine(set.Find(4));
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
