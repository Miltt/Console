using System;
using System.Collections.Generic;
using System.Threading;

namespace Philosophers
{
    public class Fork
    {
        private bool _canTake = true;
        private object _sync = new object();

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
        private const int MaxThinkDurationTime = 4000;
        private const int MaxEatDurationTime = 2000;

        private Thread _thread;
        private Fork _left;
        private Fork _right;
        private int _thinkDurationTime;
        private int _eatDurationTime;
        private bool _disposed;

        public Philosopher(Fork left, Fork right, string threadName)
        {
            _left = left;
            _right = right;

            var random = new Random();
            _thinkDurationTime = random.Next(1, MaxThinkDurationTime);
            _eatDurationTime = random.Next(1, MaxEatDurationTime);

            _thread = new Thread(Start);
            _thread.Name = threadName;
            _thread.Start();
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
            Thread.Sleep(_thinkDurationTime);            
        }

        private void Eat()
        {
            Log("Eat...");
            Thread.Sleep(_eatDurationTime);            
        }

        private void TakeForks()
        {
            _left.Take();
            Log("Take left fork");
            _right.Take();
            Log("Take right fork");
        }

        private void ReleaseForks()
        {
            _left.Release();
            Log("Release left fork");
            _right.Release();
            Log("Release right fork");
        }

        private void Log(string message)
        {
            Console.WriteLine(string.Format("Philosopher {0}\t {1}", Thread.CurrentThread.Name, message));
        }

        public void Dispose()
        {
            _disposed = true;
            Log("Disposing...");
        }
    }

    public class DiningTable : IDisposable
    {
        private int _numPhilosophers;
        private Dictionary<int, Fork> _forks;
        private List<Philosopher> _philosophers;

        public DiningTable(int numPhilosophers)
        {
            _numPhilosophers = numPhilosophers;
            _forks = new Dictionary<int, Fork>();
            _philosophers = new List<Philosopher>();
        }

        public void Create()
        {
            for (var i = 0; i < _numPhilosophers; i++)
                _forks.Add(i, new Fork());

            for (var i = 0; i < _numPhilosophers; i++)
            {
                var numFork = i == 0 ? _numPhilosophers : i;
                _philosophers.Add(new Philosopher(_forks[i], _forks[numFork - 1], i.ToString()));
            }
        }

        public void Dispose()
        {
            foreach (var p in _philosophers)
                p.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var numPhilosophers = 5;
            using (var table = new DiningTable(numPhilosophers))
            {
                table.Create();
                Console.ReadKey();
            }
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}