using System;

namespace Cnsl.DesignPatterns
{
    public abstract class Creator
    {
        public void Run()
        {
            var executor = FactoryMethod();
            if (executor is null)
                throw new InvalidOperationException($"{executor} cannot be null.");

            executor.Run();
        }

        protected abstract IExecutor FactoryMethod();
    }
}