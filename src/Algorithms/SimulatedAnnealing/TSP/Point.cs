using System;

namespace Cnsl.Algorithms.SimulatedAnnealing
{
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double GetEuclideanDistance(in Point to)
            => Math.Sqrt(Math.Pow(this.X - to.X, 2) + Math.Pow(this.Y - to.Y, 2));
    }
}