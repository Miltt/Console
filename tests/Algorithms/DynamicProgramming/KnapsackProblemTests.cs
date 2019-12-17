using System.Linq;
using Cnsl.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.DynamicProgramming
{
    [TestClass]
    public class KnapsackProblemTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var capacity = 14;
            var items = new KnapsackItem[] 
            {
                new KnapsackItem(weight: 1, value: 13),
                new KnapsackItem(weight: 6, value: 5),
                new KnapsackItem(weight: 6, value: 4),
                new KnapsackItem(weight: 5, value: 2)
            };

            var result = KnapsackProblem.Search(items, capacity);
            var backTrack = KnapsackProblem.GetBackTrack(result, items, capacity, items.Length);
            
            const int expectedSumValue = 22;

            Assert.IsTrue(backTrack.Sum(i => i.Value) == expectedSumValue, "Incorrectly found the sum of the values");
        }
    }
}