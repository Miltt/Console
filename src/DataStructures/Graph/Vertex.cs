using System;
using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public class Vertex : IVertex
    {
        private List<IEdge> _edges = new List<IEdge>();

        public IReadOnlyCollection<IEdge> Edges => _edges;
        public int Num { get; }

        public Vertex(int num)
        {
            if (num < 0)
                throw new ArgumentException("Must be at least 0", nameof(num));

            Num = num;
        }

        public void AddEdge(IEdge edge)
        {
            if (edge is null)
                throw new ArgumentNullException(nameof(edge));
            
            _edges.Add(edge);
        }

        public override string ToString()
        {
            return Num.ToString();
        }

        public bool Equals(IVertex other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            if (this.GetType() != other.GetType())
                return false;

            return Num == other.Num;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IVertex);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 17) ^ Num.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return Object.ReferenceEquals(left, null)
                ? Object.ReferenceEquals(right, null)
                : left.Equals(right);
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }
    }
}