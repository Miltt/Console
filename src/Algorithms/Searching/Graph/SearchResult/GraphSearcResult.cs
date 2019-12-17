using System;
using System.Linq;
using System.Collections.Generic;
using Cnsl.DataStructures;

namespace Cnsl.Algorithms.Searching
{
    public class GraphSearchResult
    {
        private readonly struct Visit
        {
            public const int UnknownDistance = -1;

            public bool IsVisited { get; }
            public int Distance { get; }

            public Visit(int distance)
            {
                if (distance < 0)
                    throw new ArgumentException("Must be at least 0", nameof(distance));

                IsVisited = true;
                Distance = distance;
            }

            public override string ToString()
            {
                return $"IsVisited:{IsVisited},{Distance}";
            }
        }

        private readonly Visit[] _visits;
        private readonly List<IVertex> _track;
        public IReadOnlyCollection<IVertex> Track => _track;

        public GraphSearchResult(int verticesCount)
        {
            if (verticesCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(verticesCount));

            _visits = new Visit[verticesCount];
            _track = new List<IVertex>();
        }

        public void MarkAsVisited(IVertex vertex)
        {
            ThrowIfVertexIsNull(vertex);

            _visits[vertex.Num] = new Visit(CalcDistance());
            _track.Add(vertex);
        }

        public bool IsVisited(IVertex vertex)
        {       
            ThrowIfVertexIsNull(vertex);

            return _visits[vertex.Num].IsVisited;
        }

        public int GetDistance(IVertex vertex)
        {
            ThrowIfVertexIsNull(vertex);

            var v = _visits[vertex.Num];
            return v.IsVisited
                ? v.Distance
                : Visit.UnknownDistance;
        }

        private int CalcDistance()
        {
            var lastMarkedVertex = _track.LastOrDefault();
            return lastMarkedVertex != null
                ? _visits[lastMarkedVertex.Num].Distance + 1
                : 0;
        }

        private void ThrowIfVertexIsNull(IVertex vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));
        }
    }
}