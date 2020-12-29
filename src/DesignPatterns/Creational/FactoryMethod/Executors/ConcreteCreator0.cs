namespace Cnsl.DesignPatterns
{
    public sealed class ConcreteCreator0 : Creator
    {
        protected override IExecutor FactoryMethod()
        {
            return new ConcreteExecutor0();
        }
    }
}