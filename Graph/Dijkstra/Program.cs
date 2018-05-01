using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class Dijkstra
    {
        private IPerformer _performer;

        public Dijkstra(IPerformer performer)
        {
            _performer = performer;
        }

        public void Calc(Graph graph, int source)
        {
            var distance = new int[graph.Vertexs.Count];
            for (var i = 0; i < graph.Vertexs.Count; i++)
                distance[i] = int.MaxValue;
            distance[source] = 0; // distance from source to source

            while (graph.Vertexs.Count > 0)
            {
                var vertex = GetVertex(graph, distance); // vertex with min distance[]
                graph.Vertexs.Remove(vertex);

                for (var i = 0; i < graph.Edges.Count; i++)
                {
                    if (graph.Edges[i].V == vertex)
                    {
                        var tmpDistance = distance[vertex] + GetWeight(graph, vertex, graph.Edges[i].U);
                        if (tmpDistance < distance[graph.Edges[i].U]) // a shorter path to u has been found
                            distance[graph.Edges[i].U] = tmpDistance;
                    }
                }                
            }

            _performer.Perform(distance, source);
        }

        private int GetVertex(Graph graph, int[] distance)
        {
            var vertex = int.MinValue;
            var min = int.MaxValue;

            foreach (var i in graph.Vertexs)
            {
                if (min > distance[i])
                {
                    min = distance[i];
                    vertex = i;
                }
            }            

            return vertex;
        }

        private int GetWeight(Graph graph, int v, int u)
        {
            var weigth = int.MaxValue;

            foreach (var i in graph.Edges)
            {
                if (i.V == v && i.U == u)
                {
                    weigth = i.Weigth;
                    break;
                }
            }

            return weigth;
        }
    }

    public class Graph
    {
        public List<Edge> Edges { get; private set; } = new List<Edge>();
        public HashSet<int> Vertexs { get; private set; } = new HashSet<int>();

        public void AddEdge(int vertexV, int vertexU, int weigth)
        {
            Edges.Add(new Edge { V = vertexV, U = vertexU, Weigth = weigth });
            Edges.Add(new Edge { V = vertexU, U = vertexV, Weigth = weigth });
            Vertexs.Add(vertexU);
            Vertexs.Add(vertexV);
        }
    }

    public class Edge
    {
        public int V { get; set; }
        public int U { get; set; }
        public int Weigth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            graph.AddEdge(0, 1, 7);
            graph.AddEdge(0, 2, 9);
            graph.AddEdge(0, 5, 14);
            graph.AddEdge(1, 2, 10);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 11);
            graph.AddEdge(2, 5, 2);
            graph.AddEdge(3, 4, 6);
            graph.AddEdge(4, 5, 9);

            new Dijkstra(new Performer()).Calc(graph, 0);
            
            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(int[] distance, int source);
    }

    public class Performer : IPerformer
    {
        private StringBuilder _sb = new StringBuilder();

        public void Perform(int[] distance, int source)
        {
            for (var i = 0; i < distance.Length; i++)
                _sb.AppendLine($"From {source} to {i}: {distance[i]}");

            Console.WriteLine(_sb);
        }
    }
}