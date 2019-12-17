using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class DisjointSetTests
    {
        [TestMethod]
        public void FindTest()
        {
            const int value = 2;

            var set = new DisjointSet(size: 3);
            var result = set.Find(value);

            Assert.IsTrue(result == value, "Could not find item");
        }

        [TestMethod]
        public void UnionTest()
        {
            var set = new DisjointSet(size: 5);
            var isValid = false;
            
            set.Union(0, 4);
            isValid = set.Find(0) == 0 && set.Find(4) == 0;

            set.Union(1, 3);
            isValid = isValid && set.Find(1) == 1 && set.Find(3) == 1;

            set.Union(1, 4);
            isValid = isValid && set.Find(0) == 1 && set.Find(1) == 1 && set.Find(3) == 1 && set.Find(4) == 1;

            isValid = isValid && set.Find(2) == 2;

            Assert.IsTrue(isValid, "Wrong union of sets");
        }
    }
}