using System;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public class DFS
    {
        public static GraphSearchResult Search(Graph graph, IVertex vertex)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));

            var result = new GraphSearchResult(graph.Count);
            SearchInternal(graph, vertex, result);
            return result;
        }

        private static void SearchInternal(Graph graph, IVertex vertex, GraphSearchResult result)
        {
            result.MarkAsVisited(vertex);

            foreach (var edge in vertex.Edges)
            {
                if (!result.IsVisited(edge.U))
                    SearchInternal(graph, edge.U, result);
            }
        }
    }
} 