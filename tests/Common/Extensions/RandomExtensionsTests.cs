using System;
using Cnsl.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Extensions
{
    [TestClass]
    public class RandomExtensionsTests
    {
        [TestMethod]
        public void NextLongTest()
        {
            const long minValue = long.MinValue;
            const long maxValue = long.MaxValue;

            var random = new Random();
            var value = random.Next(minValue, maxValue);

            Assert.IsTrue(value >= minValue && value <= maxValue, "Value is out of range");
        }
    }
}