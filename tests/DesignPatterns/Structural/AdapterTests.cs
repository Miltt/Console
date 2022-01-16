using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cnsl.DesignPatterns;
using System;

namespace Tests.DesignPatterns
{
    [TestClass]
    public class AdapterTests
    {
        [TestMethod]
        public void InstanceTest()
        {
            var exception = (Exception)null;
            try
            {
                var usbConnector = new UsbConnector();
                var usbToUsbCAdapter = new UsbToUsbCAdapter(usbConnector);

                var charger = new Charger(usbToUsbCAdapter);
                charger.ConnectUsbC();
                charger.Charge();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsTrue(exception is null, $"An exception was thrown: {exception?.Message}");
        }
    }
}