using System;

namespace Cnsl.Algorithms.Multithreading
{
    public class PhilosopherEventArgs : EventArgs
    {
        public string Name { get; }
        public Philosopher.EventType Type { get; }
        public string Message { get; }
        
        public PhilosopherEventArgs(string name, Philosopher.EventType type, string message)
        {
            Name = name;
            Type = type;
            Message = message;
        }
    }
}