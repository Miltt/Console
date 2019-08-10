using System;
using System.Threading;

namespace ReadersWriters
{
    public sealed class SimpleSemaphore
    {
        private const int DefaultThreads = 1;

        private readonly object _sync = new object();
        private readonly int _threadCountMax;
        private int _threadCount = 0;

        public SimpleSemaphore()
            : this(DefaultThreads) { }

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

    public class ReadersWritersProblem : IDisposable
    {
        private readonly SimpleSemaphore _semaphore = new SimpleSemaphore(threadCountMax: 2);
        private readonly SimpleSemaphore _mutex = new SimpleSemaphore();
        private readonly Writer _writer = new Writer();
        private readonly Reader _reader = new Reader();
        private int _readersCount = 0;
        private bool _disposed = false;

        public ReadersWritersProblem(int readersCount)
        {
            if (readersCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(readersCount));

            var writer = new Thread(Write);
            writer.Name = nameof(Writer);
            writer.Start();

            for (int i = 0; i < readersCount; i++)
            {
                var reader = new Thread(Read);
                reader.Name = nameof(Reader) + i;
                reader.Start();
            }
        }

        private void Write()
        {
            do 
            {                                
                _mutex.Wait();
                _writer.Write();
                _mutex.Release();

                _writer.Sleep();

            } while(!_disposed);
        }

        private void Read()
        {
            do 
            {
                _reader.Sleep();

                _semaphore.Wait();
                _readersCount++;
                if (_readersCount == 1)                   
                    _mutex.Wait();
                _semaphore.Release();       

                _reader.Read();
                
                _semaphore.Wait();
                _readersCount--;
                if (_readersCount == 0)
                    _mutex.Release();
                _semaphore.Release();

            } while (!_disposed);
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var problem = new ReadersWritersProblem(readersCount: 2))
            {
                Console.ReadKey();
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }    
    }

    public sealed class Writer : Performer
    {
        public override int ActionTimeoutMs => 3000;
        public override int SleepTimeoutMs => 1000;

        public void Write()
            => Perform(nameof(Write));
    }

    public sealed class Reader : Performer
    {
        public override int ActionTimeoutMs => 1500;
        public override int SleepTimeoutMs => 500;

        public void Read() 
            => Perform(nameof(Read));
    }

    public abstract class Performer
    {
        public abstract int ActionTimeoutMs { get; }
        public abstract int SleepTimeoutMs { get; }

        protected void Perform(string actionName)
        {
            Log(actionName);
            Thread.Sleep(ActionTimeoutMs);
        }

        public void Sleep()
        {
            Log(nameof(Sleep));
            Thread.Sleep(SleepTimeoutMs);
        }

        private void Log(string message)
            => Console.WriteLine($"{Thread.CurrentThread.Name}: {message}");
    }
}
