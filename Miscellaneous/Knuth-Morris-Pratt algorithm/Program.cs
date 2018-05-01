using System;

namespace KMPAlgorithm
{
    public class KMP
    {
        private string _pattern;
        private string _text;

        public KMP(string pattern, string text)
        {
            _pattern = pattern;
            _text = text;
        }

        private int[] GetPrefixFunc()
        {
            var result = new int[_pattern.Length];
            result[0] = 0;

            var k = 0;
            for (var i = 1; i < _pattern.Length; i++)
            {
                while (k >= 0 && _pattern[k] != _pattern[i])
                    k--;
                                
                k++;
                result[i] = k;
            }

            return result;
        }

        public int Search()
        {
            var result = -1;
            var k = 0;
            var prefixFunc = GetPrefixFunc();            

            for (int i = 0; i < _text.Length; i++)
            {
                while (k > 0 && _pattern[k] != _text[i])
                    k = prefixFunc[k - 1];

                if (_pattern[k] == _text[i])
                    k++;
                if (k == _pattern.Length)
                    return result = i - k + 1;
            }

            return result;
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {
            var text = "abcdabcabcdabcdab";
            var pattern = "abca";
            Console.WriteLine(new KMP(pattern, text).Search());

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}