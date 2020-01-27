using System.Linq;
using Cnsl.Algorithms.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Miscellaneous
{
    [TestClass]
    public class CoinsTests
    {
        [TestMethod]
        public void CalcNumCombinationsTest()
        {
            var coins = new int[] { 1, 2, 3 };
            const int amount = 5;

            var result = Coins.CountCombinations(coins, amount);
            
            const int expectedCount = 5;

            Assert.IsTrue(result == expectedCount, "Incorrect number of exchange combinations");
        }

        [TestMethod]
        public void GenerateCombinationsTest()
        {
            var coins = new int[] { 1, 2, 3 };
            const int amount = 5;

            var result = Coins.GenerateCombinations(coins, amount);

            var expectedCombinations = new Coins.Combination[]
            {
                new Coins.Combination(coin: 1, count: 0),
                new Coins.Combination(coin: 2, count: 1),
                new Coins.Combination(coin: 3, count: 1),
                new Coins.Combination(coin: 1, count: 1),
                new Coins.Combination(coin: 2, count: 2),
                new Coins.Combination(coin: 3, count: 0),
                new Coins.Combination(coin: 1, count: 2),
                new Coins.Combination(coin: 2, count: 0),
                new Coins.Combination(coin: 3, count: 1),
                new Coins.Combination(coin: 1, count: 3),
                new Coins.Combination(coin: 2, count: 1),
                new Coins.Combination(coin: 3, count: 0),
                new Coins.Combination(coin: 1, count: 5),
                new Coins.Combination(coin: 2, count: 0),
                new Coins.Combination(coin: 3, count: 0)
            };

            var isValid = result.SequenceEqual(expectedCombinations);

            Assert.IsTrue(isValid, "Incorrect combination of coins");
        }
    }
}