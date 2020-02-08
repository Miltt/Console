using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void CreateEmptyMatrixTest()
        {
            const int rowsCount = 2;
            const int columnsCount = 3;

            var matrix = new Matrix(rowsCount, columnsCount);
            var isValid = matrix.RowsCount == rowsCount && matrix.ColumnsCount == columnsCount;

            Assert.IsTrue(isValid, "The matrix has an invalid size");
        }

        [TestMethod]
        public void MatrixIndexerTest()
        {
            const int rowsCount = 2;
            const int columnsCount = 3;

            var array = new int [rowsCount, columnsCount]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            var matrix = new Matrix(array);
            
            const int i = 0;
            const int j = 2;
            
            var value = matrix[i, j];
            const int expectedValue = 3;

            Assert.IsTrue(value == expectedValue, "The expected value is not equal to the taken");
        }

        [TestMethod]
        public void CreateMatrixFromArrayTest()
        {
            const int rowsCount = 2;
            const int columnsCount = 3;

            var array = new int [rowsCount, columnsCount]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            var matrix = new Matrix(array);
            
            var isValid = matrix.RowsCount == rowsCount && matrix.ColumnsCount == columnsCount;
            if (isValid)
            {
                for (int i = 0; i < matrix.RowsCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnsCount; j++)
                    {
                        if (matrix[i, j] != array[i, j])
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (!isValid)
                        break;
                }
            }

            Assert.IsTrue(isValid, "The matrix was not created correctly");
        }

        [TestMethod]
        public void MatrixEqualsTest()
        {
            const int rowsCount = 2;
            const int columnsCount = 3;

            var array = new int [rowsCount, columnsCount]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            var matrixA = new Matrix(array);
            var matrixB = new Matrix(array);

            Assert.IsTrue(matrixA.Equals(matrixB), "Matrices are not equal");
        }

        [TestMethod]
        public void MatrixNotEqualsTest()
        {
            const int rowsCount = 2;
            const int columnsCount = 3;

            var array = new int [rowsCount, columnsCount]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            var matrixA = new Matrix(array);

            const int i = 1;
            const int j = 1;
            array[i, j] = 7;

            var matrixB = new Matrix(array);

            Assert.IsTrue(!matrixA.Equals(matrixB), "Matrices are equal");
        }

        [TestMethod]
        public void AdjacencyMatrixCreateTest()
        {
            var graph = new Graph(verticesCount: 6);
            graph.AddEdge(numV: 0, numU: 1, weight: 1);
            graph.AddEdge(numV: 0, numU: 4, weight: 1);
            graph.AddEdge(numV: 1, numU: 4, weight: 1);
            graph.AddEdge(numV: 1, numU: 2, weight: 1);
            graph.AddEdge(numV: 2, numU: 3, weight: 1);
            graph.AddEdge(numV: 3, numU: 4, weight: 1);
            graph.AddEdge(numV: 3, numU: 5, weight: 1);

            var matrix = new Matrix(graph);
            var expectedMatrix = new Matrix(new int[,] 
            {
                { 0, 1, 0, 0, 1, 0 },
                { 1, 0, 1, 0, 1, 0 },
                { 0, 1, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 1, 1 },
                { 1, 1, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0, 0 }
            });

            Assert.IsTrue(matrix.Equals(expectedMatrix), "The adjacency matrix was created incorrectly");
        }
    }
}