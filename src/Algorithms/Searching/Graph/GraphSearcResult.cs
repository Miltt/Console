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
            public IVertex Vertex { get; }
            public bool IsVisited { get; }
            public int VisitNum { get; }
            public int Distance { get; }

            public Visit(IVertex vertex, int visitNum, int distance)
            {
                if (vertex is null)
                    throw new ArgumentNullException(nameof(vertex));
                if (visitNum < 0)
                    throw new ArgumentException("Must be at least 0", nameof(visitNum));
                if (distance < 0)
                    throw new ArgumentException("Must be at least 0", nameof(distance));

                Vertex = vertex;
                IsVisited = true;
                VisitNum = visitNum;
                Distance = distance;
            }

            public override string ToString()
            {
                return $"IsVisited:{IsVisited},{Distance}";
            }
        }

        private readonly Visit[] _visits;
        private Visit _lastVisit;
        private int _visitNum;

        public GraphSearchResult(int verticesCount)
        {
            if (verticesCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(verticesCount));

            _visits = new Visit[verticesCount];
        }

        internal void MarkAsVisited(IVertex vertex)
        {
            ThrowIfVertexIsNull(vertex);

            var visit = new Visit(vertex, _visitNum++, CalcDistance());
            _visits[vertex.Num] = visit;
            _lastVisit = visit;
        }

        internal bool IsVisited(IVertex vertex)
        {
            ThrowIfVertexIsNull(vertex);

            return _visits[vertex.Num].IsVisited;
        }

        public int GetDistance(IVertex vertex)
        {
            ThrowIfVertexIsNull(vertex);

            var visit = _visits[vertex.Num];
            return visit.IsVisited
                ? visit.Distance
                : -1;
        }

        public IEnumerable<IVertex> GetTrack()
        {
            return _visits.OrderBy(v => v.VisitNum).Select(v => v.Vertex);
        }

        private int CalcDistance()
        {
            return _lastVisit.Vertex != null
                ? _visits[_lastVisit.Vertex.Num].Distance + 1
                : 0;
        }

        private void ThrowIfVertexIsNull(IVertex vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));
        }
    }
}