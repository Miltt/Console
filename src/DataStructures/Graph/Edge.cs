using System;

namespace Cnsl.DataStructures
{
    public class Edge : IEdge
    {
        public IVertex V { get; }
        public IVertex U { get; }
        public int Weight { get; }

        public Edge(IVertex v, IVertex u) 
            : this (v, u, 0) { }

        public Edge(IVertex v, IVertex u, int weight)
            : base()
        {
            V = v ?? throw new ArgumentNullException(nameof(v));
            U = u ?? throw new ArgumentNullException(nameof(u));

            if (weight < 0)
                throw new ArgumentException("Must be at least 0", nameof(weight));
            
            Weight = weight;
        }

        public int CompareTo(IEdge other)
        {
            return other == null 
                ? 1
                : Weight - other.Weight;
        }

        public override string ToString()
        {
            return $"Edge({V}-{U})";
        }

        public override bool Equals(object obj)
        {
            return obj is IEdge other
                ? V == other.V && U == other.U
                : false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 19) ^ V.GetHashCode();
                hashCode = (hashCode * 19) ^ U.GetHashCode();
                return hashCode;
            }
        }
    }
}