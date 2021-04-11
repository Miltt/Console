namespace Cnsl.DesignPatterns
{
    public sealed class ConcreteNavigator1 : IStrategy
    {
        public Route CreateRoute(Point a, Point b)
        {
            return new Route();
        }
    }
}