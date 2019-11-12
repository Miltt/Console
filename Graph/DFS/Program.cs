using System;
using System.Collections.Generic;
using System.Text;

namespace DFSGraph
{
    public class DeepFirstSearch
    {
        public readonly struct Result
        {
            private readonly bool[] _visit;
            private readonly List<Vertex> _track;
            public IReadOnlyCollection<Vertex> Track => _track;

            public Result(int verticesCount)
            {
                if (verticesCount < 0)
                    throw new ArgumentException("Must be at least 0", nameof(verticesCount));

                _visit = new bool[verticesCount];
                _track = new List<Vertex>();
            }

            public void MarkAsVisited(Vertex vertex)
            {
                if (vertex is null)
                    throw new ArgumentNullException(nameof(vertex));

                _visit[vertex.Num] = true;
                _track.Add(vertex);
            }

            public bool IsVisited(Vertex vertex)
            {       
                if (vertex is null)
                    throw new ArgumentNullException(nameof(vertex));

                return _visit[vertex.Num];
            }
        }

        public static Result Search(Graph graph, Vertex vertex)
        {
            var result = new Result(graph.Count);
            SearchInternal(graph, vertex, result);
            return result;
        }

        private static void SearchInternal(Graph graph, Vertex vertex, Result result)
        {
            result.MarkAsVisited(vertex);

            foreach (var edge in vertex.Edges)
            {
                if (!result.IsVisited(edge.U))
                    SearchInternal(graph, edge.U, result);
            }
        }
    }

    public class Graph
    {
        private Vertex[] _vertices;

        public int Count => _vertices.Length;

        public Graph(int verticesCount)
        {
            if (verticesCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(verticesCount));

            _vertices = new Vertex[verticesCount];
            for (int i = 0; i < verticesCount; i++)
                _vertices[i] = new Vertex(i);
        }

        public void AddEdge(int v, int u)
        {
            var vertexV = _vertices[v];
            var vertexU = _vertices[u];

            vertexV.AddEdge(new Edge(vertexV, vertexU));
            vertexU.AddEdge(new Edge(vertexU, vertexV));
        }

        public Vertex this[int index]
        {
            get => _vertices[index];
        }
    }

    public class Vertex 
    {
        private List<Edge> _edges = new List<Edge>();

        public IReadOnlyCollection<Edge> Edges => _edges;
        public int Num { get; }

        public Vertex(int num)
        {
            Num = num;
        }

        public void AddEdge(Edge edge)
        {
            if (edge is null)
                throw new ArgumentNullException(nameof(edge));
            
            _edges.Add(edge);
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }

    public class Edge
    {
        public Vertex V { get; }
        public Vertex U { get; }

        public Edge(Vertex v, Vertex u) 
        {
            V = v;
            U = u;
        }

        public override string ToString()
        {
            return $"{V} - {U}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph(verticesCount: 5);
            graph.AddEdge(v: 0, u: 1);
            graph.AddEdge(v: 0, u: 2);
            graph.AddEdge(v: 1, u: 3);
            graph.AddEdge(v: 2, u: 3);
            graph.AddEdge(v: 2, u: 4);
            graph.AddEdge(v: 3, u: 4);

            var result = DeepFirstSearch.Search(graph, graph[0]);
            foreach (var vertex in result.Track)
                Console.Write($"{vertex.Num} ");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}