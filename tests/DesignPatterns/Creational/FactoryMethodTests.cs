using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cnsl.DesignPatterns;
using System;

namespace Tests.DesignPatterns
{
    [TestClass]
    public class FactoryMethodTests
    {
        private class ClientTest
        {
            public void Run()
            {
                RunInternal(new ConcreteCreator0());
                RunInternal(new ConcreteCreator1());
            }

            private void RunInternal(Creator creator)
            {
                creator.Run();
            }
        }

        [TestMethod]
        public void InstanceTest()
        {
            var exception = (Exception)null;
            try
            {
                new ClientTest().Run();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsTrue(exception is null, $"An exception was thrown: {exception?.Message}");
        }
    }
}