using System;
using System.Collections.Generic;
using System.Text;

namespace GDijkstra
{
    public class Dijkstra
    {
        public static Distance Calc(Graph graph, int source)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));

            var distance = new Distance(graph.Vertices.Count, source);

            while (graph.Vertices.Count > 0)
            {
                var vertex = FindVertexByMinDistance(graph.Vertices, distance);
                graph.RemoveVertex(vertex);

                foreach (var edge in vertex.Edges)
                {                    
                    var tempDistance = distance[vertex.Num] + edge.Weigth;
                    if (tempDistance < distance[edge.U.Num]) // a shorter path to U has been found
                        distance[edge.U.Num] = tempDistance;
                }
            }

            return distance;
        }

        private static Vertex FindVertexByMinDistance(IEnumerable<Vertex> vertices, Distance distance)
        {
            var vertex = (Vertex)default;                  
            var minDistance = Distance.Infinity;

            foreach (var tempVertex in vertices)
            {
                var tempDistance = distance[tempVertex.Num];
                if (tempDistance <= minDistance)
                {
                    minDistance = tempDistance;
                    vertex = tempVertex;
                }
            } 
            
            return vertex;
        }
    }

    public class Graph
    {
        private readonly List<Vertex> _vertices;
        public IReadOnlyCollection<Vertex> Vertices => _vertices;

        public Graph(int numVertices)
        {
            _vertices = new List<Vertex>(numVertices);

            for (int i = 0; i < numVertices; i++)
                _vertices.Add(new Vertex(i));
        }

        public void AddEdge(int v, int u, int weight)
        {
            var vertexV = _vertices[v];
            var vertexU = _vertices[u];

            vertexV.AddEdge(vertexU, weight);
            vertexU.AddEdge(vertexV, weight);
        }

        public void RemoveVertex(Vertex vertex)
        {
            _vertices.Remove(vertex);
        }

        public IEnumerable<Edge> GetEdges()
        {
            foreach (var vertex in _vertices)
            {
                foreach (var edge in vertex.Edges)
                    yield return edge;
            }
        }
    }

    public readonly struct Vertex : IEquatable<Vertex>
    {
        private readonly List<Edge> _edges;
        public int Num { get; }
        public IReadOnlyCollection<Edge> Edges => _edges;

        public Vertex(int num)
        {
            if (num < 0)
                throw new ArgumentException("The vertext must be a positive number", nameof(num));
            Num = num;
            _edges = new List<Edge>();
        }

        public void AddEdge(Vertex u, int weight)
        {
            _edges.Add(new Edge(this, u, weight));
        }

        public bool Equals(Vertex other)
        {
            return Num == other.Num;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashCode.Combine(_edges, Num, Edges);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex other && this.Equals(other);
        }

        public override string ToString()
        {
            return "Vertex " + Num.ToString();
        }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }
    }

    public readonly struct Edge
    {
        public Vertex V { get; }
        public Vertex U { get; }
        public int Weigth { get; }

        public Edge(Vertex v, Vertex u, int weigth)
        {
            if (weigth < 0)
                throw new ArgumentException("Must be at least 0", nameof(weigth));

            V = v;
            U = u;
            Weigth = weigth;
        }
    }

    public readonly struct Distance
    {
        public const int Infinity = int.MaxValue;
        private const string From = "From ";
        private const string To = " to ";

        private readonly int[] _array;
        private readonly int _source;

        public Distance(int length, int source)
        {
            if (length <= 0)
                throw new ArgumentException("Must be at least 1", nameof(length));
            if (source < 0)
                throw new ArgumentException("Must be at least 0", nameof(source));

            _array = new int[length];
            _source = source;

            for (int i = 0; i < _array.Length; i++)
                _array[i] = Infinity;
            _array[_source] = 0; // distance from source to source
        }

        public int this[int index]
        {
            get { return _array[index]; }
            set { _array[index] = value; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _array.Length; i++)
            {
                var distance = _array[i];
                if (distance != Infinity)
                    sb.AppendLine($"From {_source} to {i}: {distance}");
            }

            return sb.ToString();
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph(numVertices: 15);
            graph.AddEdge(0, 1, 7);
            graph.AddEdge(0, 2, 9);
            graph.AddEdge(0, 5, 14);
            graph.AddEdge(1, 2, 10);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 11);
            graph.AddEdge(2, 5, 2);
            graph.AddEdge(3, 4, 6);
            graph.AddEdge(4, 5, 9);

            var distance = Dijkstra.Calc(graph, 0);
            Console.WriteLine(distance);
            
            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}