using System;

namespace Cnsl.Algorithms.Multithreading
{
    public class ReaderEventArgs : EventArgs
    {        
        public string Name { get; }
        public Reader.EventType Type { get; }
        public string Message { get; } 
        
        public ReaderEventArgs(string name, Reader.EventType type, string message)
        {
            Name = name;
            Type = type;
            Message = message;
        }
    }
}