using System;

namespace Cnsl.DesignPatterns
{
    public class Navigator
    {
        public IStrategy Strategy { get; set; }

        public Navigator()
        {
        }

        public Navigator(IStrategy strategy)
        {
            Strategy = strategy;
        }

        public Route GetDirection(Point a, Point b)
        {
            if (Strategy is null)
                throw new InvalidOperationException($"{nameof(Strategy)} cannot be null.");

            return Strategy.CreateRoute(a, b);
        }
    }
}