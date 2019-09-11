using System;

namespace LuckyTicket
{
    /// <summary>
    /// tram tickets have numbers of six digits from 000000 to 999999
    /// a ticket is called lucky if the sum of the 1st, 2nd and 3rd digits equals the sum of the 4th, 5th and 6th digits
    /// it is necessary to calculate the number of lucky tickets
    /// </summary>
    public sealed class Ticket
    {
        private const int MaxDigits = 9;

        public static int Naive()
        {
            var ticketsCount = 0;

            for (int d0 = 0; d0 <= MaxDigits; d0++)
            {
                for (int d1 = 0; d1 <= MaxDigits; d1++)
                {
                    for (int d2 = 0; d2 <= MaxDigits; d2++)
                    {
                        for (int d3 = 0; d3 <= MaxDigits; d3++)
                        {
                            for (int d4 = 0; d4 <= MaxDigits; d4++)
                            {
                                for (int d5 = 0; d5 <= MaxDigits; d5++)
                                {
                                    if (d0 + d1 + d2 == d3 + d4 + d5)
                                        ticketsCount++;
                                }
                            }
                        }
                    }
                }
            }

            return ticketsCount;
        }

        public static int Combinatorics()
        {
            const int M = 28; // sum of the three high-order bits in the ticket, 0 <= M <= 27

            var ticketsCount = 0;
            var part = new int[M]; // number partitions of the number M into three components

            for (int d0 = 0; d0 <= MaxDigits; d0++)
            {
                for (int d1 = 0; d1 <= MaxDigits; d1++)
                {
                    for (int d2 = 0; d2 <= MaxDigits; d2++)
                        part[d0 + d1 + d2]++;
                }
            }

            for (int m = 0; m < M; m++)
                ticketsCount += part[m] * part[m];

            return ticketsCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Ticket.Naive());
            Console.WriteLine(Ticket.Combinatorics());

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
