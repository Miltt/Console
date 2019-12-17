using System;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public readonly struct DijkstraResult
    {
        public const int Infinity = int.MaxValue;
        private readonly int[] _distance;

        public DijkstraResult(int verticesCount, IVertex source)
        {
            if (verticesCount <= 0)
                throw new ArgumentException("Must be at least 1", nameof(verticesCount));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            _distance = new int[verticesCount];

            for (int i = 0; i < _distance.Length; i++)
                _distance[i] = Infinity;
            
            _distance[source.Num] = 0; // distance from source to source
        }

        public int GetDistance(IVertex vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));

            return _distance[vertex.Num];
        }

        public void SetDistance(IVertex vertex, int distance)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));
            if (distance < 0)
                throw new ArgumentException("Must be at least 0", nameof(distance));

            _distance[vertex.Num] = distance;
        }
    }
}