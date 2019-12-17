using System;

namespace Cnsl.Algorithms.Searching
{
    public class KnuthMorrisPratt
    {
        public static int Search(string text, string pattern)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (pattern is null)
                throw new ArgumentNullException(nameof(pattern));

            if (text.Length > 0 && pattern.Length > 0)
            {
                var prefixArray = GetPrefixArray(pattern);
                var j = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    while (j > 0 && !pattern[j].Equals(text[i]))
                        j = prefixArray[j - 1];

                    if (pattern[j] == text[i])
                        j++;
                    if (j == pattern.Length)
                        return i - j + 1;
                }
            }

            return -1;
        }

        private static int[] GetPrefixArray(string pattern)
        {
            var array = new int[pattern.Length];
            var j = 0;

            for (var i = 1; i < pattern.Length; i++)
            {
                while (j >= 0 && !pattern[j].Equals(pattern[i]))
                    j--;
                                
                array[i] = ++j;
            }

            return array;
        }
    }
}