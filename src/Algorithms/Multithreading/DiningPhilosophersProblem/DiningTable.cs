using System;

namespace Cnsl.Algorithms.Multithreading
{
    public sealed class DiningTable : IDisposable
    {
        private Philosopher[] _philosophers;

        public DiningTable(int philosopherCount)
        {
            if (philosopherCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(philosopherCount));

            _philosophers = new Philosopher[philosopherCount];
            var forks = new Fork[philosopherCount];
            
            for (int i = 0; i < _philosophers.Length; i++)
            {
                var forkLeft = GetOrCreateFork(forks, i > 0 ? i - 1 : philosopherCount - 1);
                var forkRight = GetOrCreateFork(forks, i);

                var philosopher = new Philosopher(forkLeft, forkRight, i.ToString());
                _philosophers[i] = philosopher;
            }
        }

        public event EventHandler<PhilosopherEventArgs> PhilosopherEventRaised
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }

        public void Dispose()
        {
            for (int i = 0; i < _philosophers.Length; i++)
                _philosophers[i].Dispose();
            
            _philosophers = null;
        }

        private Fork GetOrCreateFork(Fork[] forks, int i)
        {
            var fork = forks[i];
            if (fork != null)
                return fork;
                
            fork = new Fork(i);
            forks[i] = fork;
            return fork;
        }

        private void AddHandler(EventHandler<PhilosopherEventArgs> handler)
        {
            for (int i = 0; i < _philosophers.Length; i++)
                _philosophers[i].EventRaised += handler;
        }

        private void RemoveHandler(EventHandler<PhilosopherEventArgs> handler)
        {
            for (int i = 0; i < _philosophers.Length; i++)
                _philosophers[i].EventRaised -= handler;
        }
    }
}