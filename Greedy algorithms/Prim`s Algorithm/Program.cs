using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace MinSpanningTree
{
    public class Graph
    {
        private Vertex[] _vertices;

        public int Count => _vertices.Length;

        public Graph(int numVertices)
        {
            if (numVertices < 0)
                throw new ArgumentException("Must be at least 0", nameof(numVertices));

            _vertices = new Vertex[numVertices];

            for (int i = 0; i < numVertices; i++)
		_vertices[i] = new Vertex();
        }

        public void AddEdge(int v, int u, int weight)
        {
            if (v < 0 || v > _vertices.Length)
                throw new ArgumentOutOfRangeException(nameof(v));
            if (u < 0 || u > _vertices.Length)
                throw new ArgumentOutOfRangeException(nameof(u));
            if (weight < 0)
                throw new ArgumentException("Must be at least 0", nameof(weight));

            _vertices[v].AddEdge(new Edge(v, u, weight));
            _vertices[u].AddEdge(new Edge(u, v, weight));
        }

        public Vertex GetVertex(int v)
        {
            if (v < 0 || v > _vertices.Length)
                throw new ArgumentOutOfRangeException(nameof(v));
            
            return _vertices[v];
        } 
    }

    public class Vertex 
    {
	private List<Edge> _edges = new List<Edge>();

	public IReadOnlyCollection<Edge> Edges => _edges;

	public void AddEdge(Edge edge)
        {
            if (edge is null)
                throw new ArgumentNullException(nameof(edge));
            
            _edges.Add(edge);
        }
    }

    public class Edge : IComparable<Edge> 
    {
        public int V { get; }
        public int U { get; }
        public int Weigth { get; }

        public Edge(int v, int u, int weigth) 
        {
            V = v;
            U = u;
            Weigth = weigth;
        }

        public int CompareTo(Edge other)
        {
            return Weigth - other.Weigth;
        }
    }

    public sealed class PrimsAlgorithm 
    {
        public static IReadOnlyCollection<Edge> GetMinSpanningTree(Graph graph, int source) 
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));

            var result = new List<Edge>();
            
            var visited = new bool[graph.Count];
            visited[source] = true;
            
            var priorityQueue = new PriorityQueue<Edge>(graph.GetVertex(source).Edges);
            while (!priorityQueue.IsEmpty())
            {
                var edge = priorityQueue.Extract();
                if (visited[edge.U])
                    continue;

                visited[edge.U] = true;
                result.Add(edge);

                foreach (var neighbor in graph.GetVertex(edge.U).Edges)
                    priorityQueue.Add(neighbor);
            }

            return result;
        }
    }

    class Programm
    {
        public static void Main(string[] args)
        {
            var graph = new Graph(numVertices: 5);
            graph.AddEdge(v: 0, u: 1, weight: 5);
            graph.AddEdge(v: 0, u: 2, weight: 3);
            graph.AddEdge(v: 0, u: 3, weight: 7); 
            graph.AddEdge(v: 1, u: 2, weight: 1);
            graph.AddEdge(v: 1, u: 3, weight: 5);
            graph.AddEdge(v: 1, u: 4, weight: 1);
            graph.AddEdge(v: 2, u: 3, weight: 1);

            var minSpanningTree = PrimsAlgorithm.GetMinSpanningTree(graph, source: 0);
            foreach (var edge in minSpanningTree)
                Console.WriteLine($"Edge: ({edge.V}, {edge.U}), Weight: {edge.Weigth}");
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public abstract class Heap<T> : IEnumerable<T>
    {
        private const int GrowMultiplier = 2;

        private T[] _heap = new T[0];
        private int _size = 0;
        private int _tail = 0;
        protected Comparer<T> _comparer;

        public int Count => _tail;
        public int Size => _size;

        protected Heap() 
            : this(Comparer<T>.Default) { }

        protected Heap(Comparer<T> comparer) 
            : this(Enumerable.Empty<T>(), comparer) { }

        protected Heap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default) { }
        
        protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if (collection == null) 
                throw new ArgumentNullException(nameof(collection));
            if (comparer == null) 
                throw new ArgumentNullException(nameof(comparer));

            _comparer = comparer;

            foreach (var item in collection)
                AddInternal(item);

            for (var i = GetParent(_tail - 1); i >= 0; i--)
                ShiftDown(i);
        }

        protected abstract bool Compare(T i, T j);

        public void Add(T item)
        {
            AddInternal(item);
            ShiftUp(_tail - 1);
        }

        public T Extract()
        {
            if (_tail == 0) 
                throw new InvalidOperationException("Heap is empty");
            
            var item = _heap[0];
            
            _tail--;
            Swap(_tail, 0);
            ShiftDown(0);
            
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _tail; i++)
                yield return _heap[i];
        }

        private void ShiftDown(int i)
        {
            var item = i;
            
            item = GetItem(LeftChild(i), item);
            item = GetItem(RightChild(i), item);
            
            if (item == i) 
                return;
            
            Swap(i, item);
            ShiftDown(item);
        }

        private void ShiftUp(int i)
        {
            if (i == 0 || Compare(_heap[GetParent(i)], _heap[i])) 
                return;

            Swap(i, GetParent(i));
            ShiftUp(GetParent(i));
        }

        private int GetItem(int i, int j)
            => i < _tail && !Compare(_heap[j], _heap[i]) ? i : j;

        private void Swap(int i, int j)
        {
            var tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }

        private void Grow()
        {
            var size = _size * GrowMultiplier + 1;
            var heap = new T[size];
            
            Array.Copy(_heap, heap, _size);
            _heap = heap;
            _size = size;
        }

        private void AddInternal(T item)
        {
            if (_tail == _size)
                Grow();

            _heap[_tail++] = item;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator(); 
        
        private static int GetParent(int i)
            => (i + 1) / 2 - 1;

        private static int LeftChild(int i)
            => (i + 1) * 2 - 1;

        private static int RightChild(int i)
            => LeftChild(i) + 1;
    }

    public sealed class PriorityQueue<T> : Heap<T>
    {
        public PriorityQueue()
            : this(Comparer<T>.Default) { }

        public PriorityQueue(Comparer<T> comparer)
            : base(comparer) { }

        public PriorityQueue(IEnumerable<T> collection) 
            : base(collection) { }

        public PriorityQueue(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer) { }

        protected override bool Compare(T i, T j)
            => _comparer.Compare(i, j) <= 0;

        public bool IsEmpty()
            => Count == 0;
    }
}
