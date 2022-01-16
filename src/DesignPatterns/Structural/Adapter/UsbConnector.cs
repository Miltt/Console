using System;

namespace Cnsl.DesignPatterns
{
    public sealed class UsbConnector : IUsb
    {
        public bool IsConnected { get; private set; }

        public void Connect()
        {
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        public void DeliverPower()
        {
            // ...
        }
    }
}