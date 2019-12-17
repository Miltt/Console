using System;
using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public class Graph
    {
        private IVertex[] _vertices;

        public IReadOnlyCollection<IVertex> Vertices => _vertices;
        public int Count => _vertices.Length;

        public Graph(int verticesCount)
        {
            if (verticesCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(verticesCount));

            _vertices = new Vertex[verticesCount];

            for (int i = 0; i < _vertices.Length; i++)
                _vertices[i] = new Vertex(i);
        }

        public void AddEdge(int numV, int numU)
            => AddEdge(numV, numU, 0);

        public void AddEdge(int numV, int numU, int weight)
            => AddEdge(_vertices[numV], _vertices[numU], weight); 

        public void AddEdge(IVertex v, IVertex u)
            => AddEdge(v, u, 0);
        
        public void AddEdge(IVertex v, IVertex u, int weight)
        {
            if (v is null)
                throw new ArgumentNullException(nameof(v));
            if (u is null)
                throw new ArgumentNullException(nameof(u));

            v.AddEdge(new Edge(v, u, weight));
            u.AddEdge(new Edge(u, v, weight));
        }

        public IVertex this[int index]
        {
            get => _vertices[index];
        }

        public void AddVertex(IVertex vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));

            Array.Resize(ref _vertices, _vertices.Length + 1);
            _vertices[_vertices.Length - 1] = vertex;
        }

        public void RemoveVertex(IVertex vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));
            
            var vertices = new IVertex[_vertices.Length - 1];
            var j = 0;
            
            for (int i = 0; i < _vertices.Length; i++)
            {
                if (_vertices[i].Num != vertex.Num)
                {
                    if (vertices.Length == j)
                        throw new ArgumentException($"The specified vertex {vertex} is not found");
                    
                    vertices[j++] = _vertices[i];
                }
            }
            
            _vertices = vertices;
        }
    }
}