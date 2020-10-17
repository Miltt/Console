using System;
using System.Collections.Generic;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public class Dijkstra
    {
        public readonly struct Distance
        {
            public static readonly Distance Infinity = new Distance(null, int.MaxValue);

            public IVertex Vertex { get; }
            public int Value { get; }

            public Distance(IVertex vertex, int value)
            {
                if (value < 0)
                    throw new ArgumentException("Must be at least 0", nameof(value));

                Vertex = vertex;
                Value = value;
            }
        }

        private readonly struct Result
        {
            private readonly Distance[] _distance;

            public Result(int verticesCount, IVertex source)
            {
                if (verticesCount <= 0)
                    throw new ArgumentException("Must be at least 1", nameof(verticesCount));
                if (source is null)
                    throw new ArgumentNullException(nameof(source));

                _distance = new Distance[verticesCount];

                for (int i = 0; i < _distance.Length; i++)
                    _distance[i] = Distance.Infinity;
                
                _distance[source.Num] = new Distance(source, 0); // distance from source to source
            }

            public int GetDistance(IVertex vertex)
            {
                if (vertex is null)
                    throw new ArgumentNullException(nameof(vertex));

                return _distance[vertex.Num].Value;
            }

            public void SetDistance(IVertex vertex, int distance)
            {
                if (vertex is null)
                    throw new ArgumentNullException(nameof(vertex));
                if (distance < 0)
                    throw new ArgumentException("Must be at least 0", nameof(distance));

                _distance[vertex.Num] = new Distance(vertex, distance);
            }

            public IEnumerable<Distance> GetDistances()
            {
                for (int i = 0; i < _distance.Length; i++)
                    yield return _distance[i];
            }
        }

        public static IEnumerable<Distance> Search(Graph graph, IVertex source)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            var result = new Result(graph.Count, source);

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

            return result.GetDistances();
        }

        private static IVertex FindVertexByMinDistance(IReadOnlyCollection<IVertex> vertices, Result result)
        {
            var vertex = (IVertex)default;
            var minDistance = Distance.Infinity.Value;

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