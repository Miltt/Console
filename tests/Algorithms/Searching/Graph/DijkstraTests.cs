using Cnsl.Algorithms.Searching;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Searching
{
    [TestClass]
    public class DijkstraTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var graph = GetGraph();
            var vertex0 = graph[0];
            var vertex1 = graph[1];
            var vertex2 = graph[2];
            var vertex3 = graph[3];
            var vertex4 = graph[4];
            var vertex5 = graph[5];

            var result = Dijkstra.Search(graph, vertex0);

            var isValidDistanse =
                result.GetDistance(vertex0) == 0 &&
                result.GetDistance(vertex1) == 7 &&
                result.GetDistance(vertex2) == 9 &&
                result.GetDistance(vertex3) == 20 &&
                result.GetDistance(vertex4) == 20 &&
                result.GetDistance(vertex5) == 11;

            Assert.IsTrue(isValidDistanse, "Distance not found correctly");
        }

        private static Graph GetGraph()
        {
            var graph = new Graph(verticesCount: 6);
            graph.AddEdge(numV: 0, numU: 1, weight: 7);
            graph.AddEdge(numV: 0, numU: 2, weight: 9);
            graph.AddEdge(numV: 0, numU: 5, weight: 14);
            graph.AddEdge(numV: 1, numU: 2, weight: 10);
            graph.AddEdge(numV: 1, numU: 3, weight: 15);
            graph.AddEdge(numV: 2, numU: 3, weight: 11);
            graph.AddEdge(numV: 2, numU: 5, weight: 2);
            graph.AddEdge(numV: 3, numU: 4, weight: 6);
            graph.AddEdge(numV: 4, numU: 5, weight: 9);
            return graph;
        }
    }
}