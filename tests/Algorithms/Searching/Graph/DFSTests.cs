using System.Collections.Generic;
using System.Linq;
using Cnsl.Algorithms.Searching;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Searching
{
    [TestClass]
    public class DFSTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var graph = new Graph(verticesCount: 5);
            graph.AddEdge(numV: 0, numU: 1);
            graph.AddEdge(numV: 0, numU: 2);
            graph.AddEdge(numV: 1, numU: 3);
            graph.AddEdge(numV: 2, numU: 3);
            graph.AddEdge(numV: 2, numU: 4);
            graph.AddEdge(numV: 3, numU: 4);

            var vertex = graph[0];
            var result = DFS.Search(graph, vertex);
            
            var expectedTrack = new List<int> { 0, 1, 3, 2, 4 };            
            var isEqual = result.Track.Select(v => v.Num).SequenceEqual(expectedTrack);
            
            Assert.IsTrue(isEqual, "Two sequences are not equal by comparing the element");
        }
    }
}