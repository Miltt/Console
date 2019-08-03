using System;
using System.Collections.Generic;
using System.Threading;

namespace Philosophers
{
    public class Fork
    {
        private readonly object _sync = new object();
        private bool _canTake = true;

        public int Num { get; }

        public Fork(int num)
        {
            Num = num;
        }

        public void Take()
        {
            lock (_sync)
            {
                if (!_canTake)
                    Monitor.Wait(_sync);

                _canTake = false;
            }
        }

        public void Release()
        {
            lock (_sync)
            {
                _canTake = true;
                Monitor.Pulse(_sync);
            }
        }
    }

    public sealed class Philosopher : IDisposable
    {
        private const int MaxThinkDurationMs = 4000;
        private const int MaxEatDurationMs = 2000;

        private readonly Fork _left;
        private readonly Fork _right;
        private readonly int _thinkDurationMs;
        private readonly int _eatDurationMs;
        private bool _disposed;

        public string Name { get; }

        public Philosopher(Fork left, Fork right, string name)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            
            Name = name;

            var random = new Random();
            _thinkDurationMs = random.Next(1, MaxThinkDurationMs);
            _eatDurationMs = random.Next(1, MaxEatDurationMs);

            var thread = new Thread(Start);
            thread.Start();
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
            Log("Think...");
            Thread.Sleep(_thinkDurationMs);            
        }

        private void Eat()
        {
            Log("Eat...");
            Thread.Sleep(_eatDurationMs);            
        }

        private void TakeForks()
        {
            Log($"Take left fork {_left.Num}");
            _left.Take();
            Log($"Take right fork {_right.Num}");
            _right.Take();
        }

        private void ReleaseForks()
        {
            Log($"Release left fork {_left.Num}");
            _left.Release();
            Log($"Release right fork {_right.Num}");
            _right.Release();
        }

        private void Log(string message)
        {
            Console.WriteLine($"Philosopher {Name}: {message}");
        }

        public void Dispose()
        {
            _disposed = true;
            Log("Disposed");
        }
    }

    public class DiningTable : IDisposable
    {
        private Philosopher[] _philosophers;

        public void Create(int numPhilosophers)
        {
            if (numPhilosophers < 0)
                throw new ArgumentException("Must be at least 0", nameof(numPhilosophers));
            if (_philosophers != null)
                Dispose();

            _philosophers = new Philosopher[numPhilosophers];
            var forks = new Fork[numPhilosophers];
            
            for (int i = 0; i < numPhilosophers; i++)
            {
                var forkLeft = GetOrCreateFork(forks, i > 0 ? i - 1 : numPhilosophers - 1);
                var forkRight = GetOrCreateFork(forks, i);

                var philosopher = new Philosopher(forkLeft, forkRight, i.ToString());

                _philosophers[i] = philosopher;
            }
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

        public void Dispose()
        {
            if (_philosophers == null)
                return;
            
            for (int i = 0; i < _philosophers.Length; i++)
                _philosophers[i]?.Dispose();
            
            _philosophers = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var table = new DiningTable())
            {
                table.Create(numPhilosophers: 5);
                Console.ReadKey();
            }
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}