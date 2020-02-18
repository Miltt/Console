using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class MatrixExtensionsTests
    {
        [TestMethod]
        public void MatrixAdditionTest()
        {
            var matrixA = new Matrix(new[,] 
            {
                { 1, 3 },
                { 1, 0 },
                { 1, 2 }
            });

            var matrixB = new Matrix(new[,] 
            {
                { 0, 0 },
                { 7, 5 },
                { 2, 1 }
            });

            var expectedMatrix = new Matrix(new[,] 
            {
                { 1, 3 },
                { 8, 5 },
                { 3, 3 }
            });

            matrixA.Addition(matrixB);

            Assert.IsTrue(matrixA.Equals(expectedMatrix), "The matrix addition is incorrect");
        }
    }
}