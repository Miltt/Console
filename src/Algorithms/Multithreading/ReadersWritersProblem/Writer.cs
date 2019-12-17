using System;
using System.Threading;

namespace Cnsl.Algorithms.Multithreading
{
    public class Writer
    {
        public enum EventType
        {
            Unknown = 0,
            Write = 1,
            Sleep = 2
        }

        public int ActionTimeoutMs => 3000;
        public int SleepTimeoutMs => 1000;

        public event EventHandler<WriterEventArgs> EventRaised;

        public void Write()
        {
            AddEvent(EventType.Write, "Write...");
            Thread.Sleep(ActionTimeoutMs);
        }

        public void Sleep()
        {
            AddEvent(EventType.Sleep, "Sleep...");
            Thread.Sleep(SleepTimeoutMs);
        }

        private void AddEvent(EventType type, string message)
            => EventRaised?.Invoke(this, new WriterEventArgs(Thread.CurrentThread.Name, type, message));
    }
}