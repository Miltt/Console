using System;
using System.Threading;

namespace Cnsl.Algorithms.Multithreading
{
    public class SimpleSemaphore
    {
        private const int DefaultThreadCount = 1;

        private readonly object _sync = new object();
        private readonly int _threadCountMax;
        private int _threadCount = 0;

        public SimpleSemaphore()
            : this(DefaultThreadCount) { }

        public SimpleSemaphore(int threadCountMax)
        {
            if (threadCountMax < 1)
                throw new ArgumentException("Must be at least 1", nameof(threadCountMax));

            _threadCountMax = threadCountMax;
        }

        public void Wait()
        {
            lock (_sync)
            {
                if (_threadCount >= _threadCountMax)
                    Monitor.Wait(_sync);

                _threadCount++;
            }
        }

        public void Release()
        {
            lock (_sync)
            {
                _threadCount--;
                
                if (_threadCount < _threadCountMax)
                    Monitor.Pulse(_sync);
            }            
        }
    }
}