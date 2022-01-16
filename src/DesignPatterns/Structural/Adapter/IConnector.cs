using System;

namespace Cnsl.DesignPatterns
{
    public interface IConnector
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
        void DeliverPower();
    }
}