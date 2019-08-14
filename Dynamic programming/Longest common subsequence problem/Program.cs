using System;

namespace LCS
{
    public class LongestCommonSubsequence
    {
        public static int[,] Find(char[] a, char[] b)
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

        public static string GetBackTrack(int[,] result, char[] a, char[] b, int i, int j)
        {        
            if (result is null || a is null || b is null)
                throw new ArgumentException("Args cannot be null");
                
            if (i == 0 || j == 0)
                return string.Empty;
            if (a[i] == b[j])
                return GetBackTrack(result, a, b, i - 1, j - 1) + a[i];
            if (result[i, j - 1] > result[i - 1, j])
                return GetBackTrack(result, a, b, i, j - 1);
            
            return GetBackTrack(result, a, b, i - 1, j);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = "abcbat".ToCharArray();
            var b = "dcbeta".ToCharArray();

            var result = LongestCommonSubsequence.Find(a, b);
            var backTrack = LongestCommonSubsequence.GetBackTrack(result, a, b, a.Length - 1, b.Length - 1);
            Console.WriteLine(backTrack);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}