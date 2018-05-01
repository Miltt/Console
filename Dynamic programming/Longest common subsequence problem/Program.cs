using System;

namespace LongestCommonSubsequenceProblem
{
    public class LongestCommonSubsequence
    {
        private int[,] _result;

        public void Solve(char[] a, char[] b)
        {
            Calc(a, b);
            Console.WriteLine(GetBackTrack(a, b, a.Length - 1, b.Length - 1));
        }

        private void Calc(char[] a, char[] b)
        {
            _result = new int[a.Length, b.Length];

            for (var i = 1; i < a.Length; i++)
            {
                for (var j = 1; j < b.Length; j++)
                {
                    _result[i, j] = a[i] == b[j]
                        ? _result[i - 1, j - 1] + 1
                        : Math.Max(_result[i, j - 1], _result[i - 1, j]);
                }
            }
        }

        private string GetBackTrack(char[] a, char[] b, int i, int j)
        {
            if (i == 0 || j == 0)
                return string.Empty;

            if (a[i] == b[j])
                return GetBackTrack(a, b, i - 1, j - 1) + a[i];
            
            if (_result[i, j - 1] > _result[i - 1, j])
                return GetBackTrack(a, b, i, j - 1);
            
            return GetBackTrack(a, b, i - 1, j);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = "abcbat".ToCharArray();
            var b = "dcbeta".ToCharArray();

            var longestSubsequence = new LongestCommonSubsequence();
            longestSubsequence.Solve(a, b);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

