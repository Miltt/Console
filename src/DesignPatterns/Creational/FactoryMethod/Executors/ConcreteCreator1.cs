namespace Cnsl.DesignPatterns
{
    public sealed class ConcreteCreator1 : Creator
    {
        protected override IExecutor FactoryMethod()
        {
            return new ConcreteExecutor1();
        }
    }
}