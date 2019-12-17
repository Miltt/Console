using System;
using System.Collections.Generic;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public class BFS
    {
        public static GraphSearchResult Search(Graph graph, IVertex source, IVertex target)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (target is null)
                throw new ArgumentNullException(nameof(target));

            var result = new GraphSearchResult(graph.Count);
            var queue = new Queue<IVertex>(graph.Count);

            result.MarkAsVisited(source);
            queue.Enqueue(source);
    
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                if (v == target)
                    break;

                foreach (var edge in v.Edges)
                {
                    var u = edge.U;
                    if (!result.IsVisited(u))
                    {
                        result.MarkAsVisited(u);
                        queue.Enqueue(u);
                    }
                }
            }

            return result;
        }
    }    
} 