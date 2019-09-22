using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdjacencyMatrix
{
    public class Graph
    {
        private Vertex[] _vertices;

        public int Count => _vertices.Length;

        public Graph(int verticesCount)
        {
            if (verticesCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(verticesCount));

            _vertices = new Vertex[verticesCount];
            
            for (int i = 0; i < _vertices.Length; i++)
		        _vertices[i] = new Vertex($"{(char)('A' + i)}");
        }

        public void AddEdge(int parent, int child, int weight)
        {
            var parentVertex = GetVertex(parent);
            var childVertex = GetVertex(child);
            if (!parentVertex.Equals(default) && !childVertex.Equals(default))
                parentVertex.AddEdge(childVertex, weight);
        }

        public Vertex GetVertex(int i)
        {
            var vertex = _vertices[i];
            if (vertex == null)
                throw new InvalidOperationException($"Vertex not found by index {i}");
            return vertex;
        }
    }

    public class Vertex
    {
        private List<Edge> _edges = new List<Edge>();

        public string Name { get; }
        public IReadOnlyCollection<Edge> Edges => _edges;

        public Vertex(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddEdge(Vertex child, int weight)
        {
            if (child is null)
                throw new ArgumentNullException(nameof(child));
            if (weight < 0)
                throw new ArgumentException("Must be at least 0", nameof(weight));

            _edges.Add(new Edge(weight, this, child));                

            if (!child.Edges.Any(a => ReferenceEquals(a.Parent, child) && ReferenceEquals(a.Child, this)))
                child.AddEdge(this, weight);
        }
    }

    public readonly struct Edge : IEquatable<Edge>
    {
        public int Weight { get; }
        public Vertex Parent { get; }
        public Vertex Child { get; }

        public Edge(int weight, Vertex parent, Vertex child)
        {
            if (weight < 0)
                throw new ArgumentException("Must be at least 0", nameof(weight));

            Weight = weight;
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Child = child ?? throw new ArgumentNullException(nameof(child));
        }

        public bool Equals(Edge other)
        {
            return Weight == other.Weight
                && ReferenceEquals(Parent, other.Parent)
                && ReferenceEquals(Child, other.Child);
        }
    }

    public class AdjacencMatrix
    {
        public static Matrix Convert(Graph graph)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));

            var matrix = new Matrix(graph.Count);

            for (int i = 0; i < graph.Count; i++)
            {
                var parent = graph.GetVertex(i);
                for (int j = 0; j < graph.Count; j++)
                {
                    var child = graph.GetVertex(j);                   
                    var edge = parent.Edges.FirstOrDefault(e => ReferenceEquals(e.Child, child));
                    if (!edge.Equals(default))
                        matrix.Grid[i, j] = edge.Weight;
                }
            }

            return matrix;
        }
    }

    public readonly struct Matrix
    {
        public readonly int[,] Grid;
        public readonly int Size;

        public Matrix(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Must be at least 1", nameof(size));

            Grid = new int[size, size];
            Size = size;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("    ");

            for (int i = 0; i < Size; i++)
                sb.Append($"{(char)('A' + i)}  ");

            sb.AppendLine();

            for (int i = 0; i < Size; i++)
            {
                sb.Append($"{(char)('A' + i)} [");

                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                        sb.Append(" - ");
                    else
                        sb.Append($" {Grid[i, j]} ");
                }

                sb.Append("]" + Environment.NewLine);
            }

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph(verticesCount: 8);
            graph.AddEdge(parent: 0, child: 0, weight: 0);
            graph.AddEdge(parent: 0, child: 1, weight: 1);
            graph.AddEdge(parent: 0, child: 3, weight: 1);
            graph.AddEdge(parent: 1, child: 4, weight: 1);
            graph.AddEdge(parent: 1, child: 3, weight: 3);
            graph.AddEdge(parent: 2, child: 5, weight: 1);
            graph.AddEdge(parent: 2, child: 3, weight: 3);
            graph.AddEdge(parent: 3, child: 7, weight: 8);
            graph.AddEdge(parent: 4, child: 6, weight: 1);
            graph.AddEdge(parent: 4, child: 7, weight: 3);
            graph.AddEdge(parent: 5, child: 7, weight: 3);
 
            var matrix = AdjacencMatrix.Convert(graph);
            Console.WriteLine(matrix);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}