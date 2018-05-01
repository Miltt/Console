using System;
using System.Collections.Generic;

namespace BFS
{
    public class BreadthFirstSearch
    {
        private Graph _graph;
        private bool[] _visited;
        private Queue<int> _queue = new Queue<int>();

        public int[] Distance { get; private set; }

        public BreadthFirstSearch(Graph graph)
        {
            _graph = graph;
            _visited = new bool[graph.Vertexs.Count];
            Distance = new int[graph.Vertexs.Count];
        }

        public bool Start(int sourceVertex, int targetVertex)
        {
            MarkAsVisited(sourceVertex, 0);

            while (_queue.Count > 0)
            {
                var vertex = _queue.Dequeue();
                if (vertex == targetVertex)                
                    return true;

                for (var i = 0; i < _graph.Edges.Count; i++)
                {    
                    if (_graph.Edges[i].U == vertex && !_visited[_graph.Edges[i].V])
                        MarkAsVisited(_graph.Edges[i].V, Distance[vertex] + 1);
                }
            }

            return false;
        }

        private void MarkAsVisited(int vertex, int distance)
        {
            Distance[vertex] = distance;
            _queue.Enqueue(vertex);
            _visited[vertex] = true;
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

            var bfs = new BreadthFirstSearch(graph);
            var isReachable = bfs.Start(1, 4);
            if (isReachable)
                Console.WriteLine(bfs.Distance[4]);
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
