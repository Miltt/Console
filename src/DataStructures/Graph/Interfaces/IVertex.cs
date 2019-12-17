using System;
using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public interface IVertex : IEquatable<IVertex>
    {
        IReadOnlyCollection<IEdge> Edges { get; }
        int Num { get; }
        void AddEdge(IEdge edge);
    }
}