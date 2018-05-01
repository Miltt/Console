using System;
using System.Collections.Generic;
using System.Text;

namespace DFS
{
    public class DeepFirstSearch
    {
        private Graph _graph;
        private bool[] _visited;
        private IPerformer _performer;

        public DeepFirstSearch(Graph graph, IPerformer performer)
        {
            _graph = graph;
            _performer = performer;
            _visited = new bool[graph.Vertexs.Count];
        }

        public void Start(int vertex)
        {
            _visited[vertex] = true;
            _performer.Perform(vertex);

            for (var i = 0; i < _graph.Edges.Count; i++)
            {
                if (_graph.Edges[i].U == vertex && !_visited[_graph.Edges[i].V])                    
                    Start(_graph.Edges[i].V);                    
            }
        }
    }

    public class Graph
    {
        public List<Edge> Edges { get; private set; }
        public HashSet<int> Vertexs { get; private set; }

        public Graph()
        {
            Edges = new List<Edge>();
            Vertexs = new HashSet<int>();
        }

        public void AddEdge(int u, int v)
        {
            Edges.Add(new Edge { U = u, V = v });
            Edges.Add(new Edge { U = v, V = u });
            Vertexs.Add(u);
            Vertexs.Add(v);
        }
    }

    public class Edge
    {
        public int U { get; set; }
        public int V { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);

            var performer = new Performer();
            new DeepFirstSearch(graph, performer).Start(0);
            Console.WriteLine(performer.Result);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(int data);
    }

    public class Performer : IPerformer
    {
        public StringBuilder Result { get; private set; } = new StringBuilder();

        public void Perform(int data)
        {
            Result.AppendFormat($"{data} ");
        }
    }
}
