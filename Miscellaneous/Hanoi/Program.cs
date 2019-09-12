using System;

namespace Hanoi
{
    public static class TowerOfHanoi
    {
        public static void Solve(int diskCount, int from, int to, int buffer)
        {
            if (diskCount >= 1)
            {
                Solve(diskCount - 1, from, buffer, to);
                MoveDisk(from, to);
                Solve(diskCount - 1, buffer, to, from);
            }
        }

        private static void MoveDisk(int from, int to)
            => Console.WriteLine($"Move top disc from {from} to {to}");
    }

    class Program
    {        
        static void Main(string[] args)
        {
            TowerOfHanoi.Solve(3, 1, 3, 2);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}