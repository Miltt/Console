using System;
using System.Collections.Generic;

namespace BFSGraph
{
    public class BreadthFirstSearch
    {
        public readonly struct Visit
        {
            public bool IsVisited { get; }
            public int Distance { get; }

            public Visit(int distance)
            {
                if (distance < 0)
                    throw new ArgumentException("Must be at least 0", nameof(distance));

                IsVisited = true;
                Distance = distance;
            }
        }

        public static bool TryGetDistance(Graph graph, Vertex source, Vertex target, out Visit[] visit)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (target is null)
                throw new ArgumentNullException(nameof(target));

            visit = new Visit[graph.Count];
            var queue = new Queue<Vertex>(graph.Count);

            MarkAsVisited(source, 0, queue, visit);
    
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                if (v == target)                
                    return true;

                foreach (var edge in v.Edges)
                {
                    var u = edge.U;
                    if (!visit[u.Num].IsVisited)
                        MarkAsVisited(u, visit[v.Num].Distance + 1, queue, visit);
                }
            }

            return false;
        }

        private static void MarkAsVisited(Vertex vertex, int distance, Queue<Vertex> queue, Visit[] visit)
        {
            visit[vertex.Num] = new Visit(distance);
            queue.Enqueue(vertex);
        }
    }

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
            var graph = new Graph(numVertices: 5);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);

            var source = graph[0];
            var target = graph[4];

            if (BreadthFirstSearch.TryGetDistance(graph, source, target, out BreadthFirstSearch.Visit[] visit))
                Console.WriteLine(visit[target.Num].Distance);
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}