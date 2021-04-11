using System;

namespace Cnsl.DesignPatterns
{
    public readonly struct Route
    {
        public Point[] Points { get; }

        public Route(Point[] points)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }
    }
}