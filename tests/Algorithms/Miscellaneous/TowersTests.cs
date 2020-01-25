using System.Collections.Generic;
using System.Linq;
using Cnsl.Algorithms.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Miscellaneous
{
    [TestClass]
    public class TowersTest
    {
        [TestMethod]
        public void MovesTest()
        {
            var result = Towers.Run(diskCount: 3);

            const int expectedMoveCount = 7;
            var expectedResult = new List<Towers.Move>()
            {
                new Towers.Move(from: 1, to: 3),
                new Towers.Move(from: 1, to: 2),
                new Towers.Move(from: 3, to: 2),
                new Towers.Move(from: 1, to: 3),
                new Towers.Move(from: 2, to: 1),
                new Towers.Move(from: 2, to: 3),
                new Towers.Move(from: 1, to: 3)
            };

            var isValid = expectedMoveCount == result.Count && result.SequenceEqual(expectedResult);
            
            Assert.IsTrue(isValid, "Moving disks is wrong");
        }
    }
}