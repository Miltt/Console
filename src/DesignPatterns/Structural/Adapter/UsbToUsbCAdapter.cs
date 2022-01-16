using System;

namespace Cnsl.DesignPatterns
{
    public sealed class UsbToUsbCAdapter : IUsbC
    {
        private readonly IUsb _usb;

        public bool IsConnected => _usb.IsConnected;

        public UsbToUsbCAdapter(IUsb usb)
        {
            _usb = usb ?? throw new ArgumentNullException(nameof(usb));
        }

        public void Connect()
        {
            _usb.Connect();
        }

        public void Disconnect()
        {
            _usb.Disconnect();
        }

        public void DeliverPower()
        {
            _usb.DeliverPower();
        }
    }
}