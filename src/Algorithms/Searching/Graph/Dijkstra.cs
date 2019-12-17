using System;
using System.Collections.Generic;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public class Dijkstra
    {
        public static DijkstraResult Search(Graph graph, IVertex source)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));

            var result = new DijkstraResult(graph.Count, source);

            while (graph.Count > 0)
            {
                var vertex = FindVertexByMinDistance(graph.Vertices, result);
                graph.RemoveVertex(vertex);

                foreach (var edge in vertex.Edges)
                {                    
                    var tempDistance = result.GetDistance(vertex) + edge.Weight;
                    if (tempDistance < result.GetDistance(edge.U)) // a shorter path to U
                        result.SetDistance(edge.U, tempDistance);
                }
            }

            return result;
        }

        private static IVertex FindVertexByMinDistance(IReadOnlyCollection<IVertex> vertices, DijkstraResult result)
        {
            var vertex = (IVertex)default;                  
            var minDistance = DijkstraResult.Infinity;

            foreach (var tempVertex in vertices)
            {
                var tempDistance = result.GetDistance(tempVertex);
                if (tempDistance <= minDistance)
                {
                    minDistance = tempDistance;
                    vertex = tempVertex;
                }
            } 
            
            return vertex;
        }
    }
} 