using System.Threading;

namespace Cnsl.Algorithms.Multithreading
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
}