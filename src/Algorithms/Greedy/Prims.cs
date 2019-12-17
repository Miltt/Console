using System;
using System.Collections.Generic;
using Cnsl.Common.Collections;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Greedy
{
    public class Prims
    {
        public static IReadOnlyCollection<IEdge> GetMinSpanningTree(Graph graph, IVertex source) 
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));

            var result = new List<IEdge>();
            
            var visited = new bool[graph.Count];
            visited[source.Num] = true;

            var priorityQueue = new PriorityQueue<IEdge>(graph[source.Num].Edges);
            while (!priorityQueue.IsEmpty())
            {
                var edge = priorityQueue.Extract();
                if (visited[edge.U.Num])
                    continue;

                visited[edge.U.Num] = true;
                result.Add(edge);

                foreach (var neighbor in graph[edge.U.Num].Edges)
                    priorityQueue.Add(neighbor);
            }

            return result;
        }
    }
}