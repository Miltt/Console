using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cnsl.DesignPatterns;
using System;

namespace Tests.DesignPatterns
{
    [TestClass]
    public class StrategyTests
    {
        private class ClientTest
        {
            public void Run()
            {
                var navigator = new Navigator(new ConcreteNavigator0());
                var a = new Point(1, 1);
                var b = new Point(4, 4);

                var route0 = navigator.GetDirection(a, b);

                navigator.Strategy = new ConcreteNavigator1();
                var route1 = navigator.GetDirection(a, b);
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