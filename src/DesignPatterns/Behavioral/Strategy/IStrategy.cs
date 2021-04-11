namespace Cnsl.DesignPatterns
{
    public interface IStrategy
    {
        Route CreateRoute(Point a, Point b);
    }
}