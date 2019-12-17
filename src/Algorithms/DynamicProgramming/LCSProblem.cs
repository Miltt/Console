using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.DynamicProgramming
{
    public class LCSProblem
    {
        public static int[,] Search(char[] a, char[] b)
        {
            if (a is null)
                throw new ArgumentNullException(nameof(a));
            if (b is null)
                throw new ArgumentNullException(nameof(b));

            var result = new int[a.Length, b.Length];

            for (int i = 1; i < a.Length; i++)
            {
                for (int j = 1; j < b.Length; j++)
                {
                    result[i, j] = a[i] == b[j]
                        ? result[i - 1, j - 1] + 1
                        : Math.Max(result[i, j - 1], result[i - 1, j]);
                }
            }

            return result;
        }

        public static IEnumerable<char> GetBackTrack(int[,] result, char[] a, char[] b, int i, int j)
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));
            if (a is null)
                throw new ArgumentNullException(nameof(a));
            if (b is null)
                throw new ArgumentNullException(nameof(b));

            if (i != 0 && j != 0)
            {
                if (a[i] == b[j])
                {
                    foreach (var item in GetBackTrack(result, a, b, i - 1, j - 1))
                        yield return item;
                    
                    yield return a[i];
                }
                else if (result[i, j - 1] > result[i - 1, j])
                {
                    foreach (var item in GetBackTrack(result, a, b, i, j - 1))
                        yield return item;
                }
                else 
                {
                    foreach (var item in GetBackTrack(result, a, b, i - 1, j))
                        yield return item; 
                }
            }
        }
    }
}