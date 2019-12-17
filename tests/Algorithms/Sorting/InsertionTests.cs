using Cnsl.Algorithms.Sorting;
using Cnsl.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Sorting
{
    [TestClass]
    public class InsertionTests
    {
        [TestMethod]
        public void SortAscTest()
        {
            var array = new int[] { 8, 2, 3, 5, 6, 1, 7, 4, 0, 9 };

            new Insertion().Sort(array);

            Assert.IsTrue(array.IsSortedByAsc(), "The array is not sorted ascending");
        }
    }
}
