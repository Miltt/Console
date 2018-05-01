using System;
using System.Collections.Generic;
using System.Linq;

namespace MinSpanningTree
{
    public sealed class PrimsAlgorithm
    {
        public List<Edge> GetMinSpanningTree(Graph graph)
        {
            var result = new List<Edge>();

            var unselectedEdges = graph.Edges;
            var unselectedVertexs = graph.Vertexs;

            var vertex = new Random().Next(0, graph.Vertexs.Count());
            var selectedVertexs = new HashSet<int>();
            selectedVertexs.Add(vertex);
            unselectedVertexs.Remove(vertex);

            while (unselectedVertexs.Count > 0)
            {
                var index = 0;

                // search the minimum-weight edge
                for (var i = 0; i < unselectedEdges.Count; i++)
                {
                    if ((selectedVertexs.Contains(unselectedEdges[i].U)) && (unselectedVertexs.Contains(unselectedEdges[i].V)) ||
                        (selectedVertexs.Contains(unselectedEdges[i].V)) && (unselectedVertexs.Contains(unselectedEdges[i].U)))
                    {
                        if (unselectedEdges[i].Weight < unselectedEdges[index].Weight)
                            index = i;
                    }
                }

                // add a new vertex (U or V) to the selected list and remove from list unselected
                if (selectedVertexs.Contains(unselectedEdges[index].U))
                {
                    selectedVertexs.Add(unselectedEdges[index].V);
                    unselectedVertexs.Remove(unselectedEdges[index].V);
                }
                else
                {
                    selectedVertexs.Add(unselectedEdges[index].U);
                    unselectedVertexs.Remove(unselectedEdges[index].U);
                }

                // add a new edge to the tree and delete from the list of unselected
                result.Add(unselectedEdges[index]);
                unselectedEdges.RemoveAt(index);                
            }

            return result;
        }
    }

    class Programm
    {
        public static void Main(string[] args)
        {
            var graph = new Graph();
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(0, 2, 3);
            graph.AddEdge(0, 3, 7);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 4, 1);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(2, 3, 1);

            var result = new PrimsAlgorithm().GetMinSpanningTree(graph);
            foreach (var r in result)
            {
                Console.WriteLine(String.Format("Edge ({0}, {1})", r.U, r.V));
            }
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
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
        
        public virtual void AddEdge(int u, int v, int weight)
        {
            // undirected edges
            Edges.Add(new Edge(u, v, weight));
            Edges.Add(new Edge(v, u, weight));
            Vertexs.Add(u);
            Vertexs.Add(v);
        }
    }

    public sealed class Edge
    {
        public int U { get; private set; }
        public int V { get; private set; }
        public int Weight { get; private set; }

        public Edge(int u, int v, int weight)
        {
            U = u;
            V = v;
            Weight = weight;
        }       
    }
}
