using System;

namespace Cnsl.DataStructures
{
    public interface IEdge : IComparable<IEdge>, IEquatable<IEdge>
    {
        IVertex V { get; }
        IVertex U { get; }
        int Weight { get; }
    }
}