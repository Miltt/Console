using System.Collections.Generic;
using System.Linq;
using Cnsl.Algorithms.Searching;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Searching
{
    [TestClass]
    public class BFSTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var graph = GetGraph();

            var source = graph[0];
            var target = graph[4];
            var result = BFS.Search(graph, source, target);

            var expectedTrack = new List<int> { 0, 1, 2, 3, 4 };
            var isEqual = result.GetTrack().Select(v => v.Num).SequenceEqual(expectedTrack);

            Assert.IsTrue(isEqual, "Two sequences are not equal by comparing the element");
        }
        
        [TestMethod]
        public void CalculateDistanceTest()
        {
            var graph = GetGraph();

            var source = graph[0];
            var target = graph[4];
            var result = BFS.Search(graph, source, target);
            
            const int expectedDistance = 4;

            Assert.IsTrue(result.GetDistance(target) == expectedDistance, "The calculated distance is not valid");
        }

        private static Graph GetGraph()
        {
            var graph = new Graph(verticesCount: 5);
            graph.AddEdge(numV: 0, numU: 1);
            graph.AddEdge(numV: 0, numU: 2);
            graph.AddEdge(numV: 1, numU: 3);
            graph.AddEdge(numV: 2, numU: 3);
            graph.AddEdge(numV: 2, numU: 4);
            graph.AddEdge(numV: 3, numU: 4);
            return graph;
        }
    }
}