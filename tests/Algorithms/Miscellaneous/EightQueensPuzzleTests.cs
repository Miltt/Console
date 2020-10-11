using Cnsl.Algorithms.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Miscellaneous
{
    [TestClass]
    public class EightQueensPuzzleTests
    {
        [TestMethod]
        public void SolutionTest()
        {
            const int boardSize = 8;
            EightQueensPuzzle.TryFind(boardSize, out var result);

            var solution1 = new bool[boardSize, boardSize];
            solution1[0, 0] = true;
            solution1[1, 4] = true;
            solution1[2, 7] = true;
            solution1[3, 5] = true;
            solution1[4, 2] = true;
            solution1[5, 6] = true;
            solution1[6, 1] = true;
            solution1[7, 3] = true;

            var isValid = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (solution1[i, j] != result[i, j])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                    break;
            }

            Assert.IsTrue(isValid, "Wrong solution");
        }
    }
}