using System.Linq;
using Cnsl.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.DynamicProgramming
{
    [TestClass]
    public class LCSProblemTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var a = "abcbat".ToCharArray();
            var b = "dcbeta".ToCharArray();

            var result = LCSProblem.Search(a, b);
            var backTrack = LCSProblem.GetBackTrack(result, a, b, a.Length - 1, b.Length - 1);

            var expectedTrack = new char[] { 'c', 'b', 'a' };            
            var isEqual = backTrack.SequenceEqual(expectedTrack);

            Assert.IsTrue(isEqual, "The sequence found is incorrect");
        }
    }
}