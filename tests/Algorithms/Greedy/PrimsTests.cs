using System.Linq;
using Cnsl.Algorithms.Greedy;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Greedy
{
    [TestClass]
    public class PrimsTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var graph = new Graph(verticesCount: 5);
            graph.AddEdge(numV: 0, numU: 1, weight: 5);
            graph.AddEdge(numV: 0, numU: 2, weight: 3);
            graph.AddEdge(numV: 0, numU: 3, weight: 7);
            graph.AddEdge(numV: 1, numU: 2, weight: 1);
            graph.AddEdge(numV: 1, numU: 3, weight: 5);
            graph.AddEdge(numV: 1, numU: 4, weight: 1);
            graph.AddEdge(numV: 2, numU: 3, weight: 1);

            var source = graph[0];
            var minSpanningTree = Prims.GetMinSpanningTree(graph, source);
            
            var treeWeight = minSpanningTree.Count > 0
                ? minSpanningTree.Sum(e => e.Weight)
                : 0;
            const int minWeight = 6;
            
            Assert.IsTrue(treeWeight == minWeight);
        }
    }
}