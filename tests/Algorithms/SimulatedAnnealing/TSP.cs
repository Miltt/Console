using Cnsl.Algorithms.SimulatedAnnealing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.SimulatedAnnealing
{
    [TestClass]
    public class TSPTests
    {
        [TestMethod]
        public void OptimizationTest()
        {
            var points = new TSP.Point[]
            {
                new TSP.Point(500, 500),
                new TSP.Point(2000, 250),
                new TSP.Point(1000, 2500),
                new TSP.Point(1500, 1200),
                new TSP.Point(2465, 1779)
            };

            var result = new TSP().Optimization(points, startTemperature: 100, endTemperature: 0.000001);
            
            const int expectedEnergy = 7365;

            Assert.IsTrue((int)result.Energy == expectedEnergy, "Incorrect answer");
        }
    }
}