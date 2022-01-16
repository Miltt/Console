using System;

namespace Cnsl.DesignPatterns
{
    public sealed class Charger
    {
        private readonly IUsbC _usbCConnector;

        public Charger(IUsbC usbCConnector)
        {
            _usbCConnector = usbCConnector ?? throw new ArgumentNullException(nameof(usbCConnector));
        }

        public void ConnectUsbC()
        {
            _usbCConnector.Connect();
        }

        public void DisconnectUsbC()
        {
            _usbCConnector.Disconnect();
        }

        public void Charge()
        {
            if (_usbCConnector.IsConnected)
            {
                _usbCConnector.DeliverPower();
            }
            else
            {
                throw new InvalidOperationException("The Usb is not connected.");
            }
        }
    }
}