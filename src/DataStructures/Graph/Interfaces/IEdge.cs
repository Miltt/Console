using System;

namespace Cnsl.DataStructures
{
    public interface IEdge : IComparable<IEdge>
    {
        IVertex V { get; }
        IVertex U { get; }
        int Weight { get; }
    }
}