using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public interface IGraph
    {
        IReadOnlyCollection<IVertex> Vertices { get; }
        int Count { get; }

        void AddEdge(int numV, int numU);
        void AddEdge(int numV, int numU, int weight);
        void AddEdge(IVertex v, IVertex u);
        void AddEdge(IVertex v, IVertex u, int weight);
        IVertex this[int index] { get; }
        void AddVertex(IVertex vertex);
        void RemoveVertex(IVertex vertex);
    }
}