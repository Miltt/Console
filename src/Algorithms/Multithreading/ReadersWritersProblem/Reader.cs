using System;
using System.Threading;

namespace Cnsl.Algorithms.Multithreading
{
    public class Reader
    {
        public enum EventType
        {
            Unknown = 0,
            Read = 1,
            Sleep = 2
        }

        public int ActionTimeoutMs => 1500;
        public int SleepTimeoutMs => 500;

        public event EventHandler<ReaderEventArgs> EventRaised;

        public void Read()
        {
            AddEvent(EventType.Read, "Read...");
            Thread.Sleep(ActionTimeoutMs);
        }

        public void Sleep()
        {
            AddEvent(EventType.Sleep, "Sleep...");
            Thread.Sleep(SleepTimeoutMs);
        }

        private void AddEvent(EventType type, string message)
            => EventRaised?.Invoke(this, new ReaderEventArgs(Thread.CurrentThread.Name, type, message));
    }
}