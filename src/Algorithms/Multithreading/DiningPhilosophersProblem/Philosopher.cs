using System;
using System.Threading;

namespace Cnsl.Algorithms.Multithreading
{
    public sealed class Philosopher : IDisposable
    {
        public enum EventType
        {
            Unknown = 0,
            Think = 1,
            TakeLeftFork = 2,
            TakeRightFork = 3,
            Eat = 4,
            ReleaseLeftFork = 5,
            ReleaseRightFork = 6,
            Disposed = 7
        }

        private const int MaxThinkDurationMs = 2000;
        private const int MaxEatDurationMs = 1000;

        private readonly Fork _left;
        private readonly Fork _right;
        private readonly int _thinkDurationMs;
        private readonly int _eatDurationMs;
        private bool _disposed;

        public string Name { get; }

        public event EventHandler<PhilosopherEventArgs> EventRaised;

        public Philosopher(Fork left, Fork right, string name)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            
            Name = name ?? throw new ArgumentNullException(nameof(name));

            var random = new Random();
            _thinkDurationMs = random.Next(1, MaxThinkDurationMs);
            _eatDurationMs = random.Next(1, MaxEatDurationMs);

            var thread = new Thread(Start);
            thread.Start();
        }

        public void Dispose()
        {
            _disposed = true;
            AddEvent(EventType.Disposed, "Disposed");
            UnsubscribeHandlers();
        }

        private void Start()
        {
            while (!_disposed)
            {
                Think();
                TakeForks();
                Eat();
                ReleaseForks();
            }
        }

        private void Think()
        {
            AddEvent(EventType.Think, "Think...");
            Thread.Sleep(_thinkDurationMs);            
        }

        private void Eat()
        {
            AddEvent(EventType.Eat, "Eat...");
            Thread.Sleep(_eatDurationMs);            
        }

        private void TakeForks()
        {
            AddEvent(EventType.TakeLeftFork, $"Num:{_left.Num}");
            _left.Take();
            AddEvent(EventType.TakeRightFork, $"Num:{_right.Num}");
            _right.Take();
        }

        private void ReleaseForks()
        {
            AddEvent(EventType.ReleaseLeftFork, $"Num:{_left.Num}");
            _left.Release();
            AddEvent(EventType.ReleaseRightFork, $"Num:{_right.Num}");
            _right.Release();
        }

        private void AddEvent(EventType type, string message)
        {            
            EventRaised?.Invoke(this, new PhilosopherEventArgs(Name, type, message));
        }

        private void UnsubscribeHandlers()
        {
            if (EventRaised != null)
            {
                foreach (var handler in EventRaised.GetInvocationList())
                    EventRaised -= (handler as EventHandler<PhilosopherEventArgs>);
            }
        } 
    }
}