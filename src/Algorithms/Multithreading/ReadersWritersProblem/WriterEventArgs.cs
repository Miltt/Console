using System;

namespace Cnsl.Algorithms.Multithreading
{
    public class WriterEventArgs : EventArgs
    {        
        public string Name { get; }
        public Writer.EventType Type { get; }
        public string Message { get; } 
        
        public WriterEventArgs(string name, Writer.EventType type, string message)
        {
            Name = name;
            Type = type;
            Message = message;
        }
    }
}