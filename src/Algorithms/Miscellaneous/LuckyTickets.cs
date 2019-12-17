namespace Cnsl.Algorithms.Miscellaneous
{
    /// <summary>
    /// tram tickets have numbers of six digits from 000000 to 999999
    /// a ticket is called lucky if the sum of the 1st, 2nd and 3rd digits equals the sum of the 4th, 5th and 6th digits
    /// it is necessary to calculate the number of lucky tickets
    /// </summary>
    public class LuckyTickets
    {
        private const int MaxDigitValue = 9;

        public static int Naive()
        {
            var count = 0;

            for (int d0 = 0; d0 <= MaxDigitValue; d0++)
            {
                for (int d1 = 0; d1 <= MaxDigitValue; d1++)
                {
                    for (int d2 = 0; d2 <= MaxDigitValue; d2++)
                    {
                        for (int d3 = 0; d3 <= MaxDigitValue; d3++)
                        {
                            for (int d4 = 0; d4 <= MaxDigitValue; d4++)
                            {
                                for (int d5 = 0; d5 <= MaxDigitValue; d5++)
                                {
                                    if (d0 + d1 + d2 == d3 + d4 + d5)
                                    {
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return count;
        }

        public static int Combinatorics()
        {
            const int MaxSum = 28; // sum of the three high-order bits in the ticket, 0 <= M <= 27

            var count = 0;
            var part = new int[MaxSum]; // number partitions of the number M into three components

            for (int d0 = 0; d0 <= MaxDigitValue; d0++)
            {
                for (int d1 = 0; d1 <= MaxDigitValue; d1++)
                {
                    for (int d2 = 0; d2 <= MaxDigitValue; d2++)
                    {
                        part[d0 + d1 + d2]++;
                    }
                }
            }

            for (int i = 0; i < part.Length; i++)
            {
                var value = part[i];
                count += value * value;
            }

            return count;
        }
    }
}