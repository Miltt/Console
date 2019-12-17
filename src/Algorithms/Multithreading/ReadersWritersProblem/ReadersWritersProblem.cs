using System;
using System.Threading;

namespace Cnsl.Algorithms.Multithreading
{
    public sealed class ReadersWritersProblem : IDisposable
    {
        private readonly SimpleSemaphore _semaphore = new SimpleSemaphore(threadCountMax: 2);
        private readonly SimpleSemaphore _mutex = new SimpleSemaphore();
        private readonly Writer _writer = new Writer();
        private readonly Reader _reader = new Reader();
        private int _readersCount = 0;
        private bool _disposed = false;

        public event EventHandler<WriterEventArgs> WriterEventRaised
        {
            add { _writer.EventRaised += value; }
            remove { _writer.EventRaised -= value; }
        }

        public event EventHandler<ReaderEventArgs> ReaderEventRaised
        {
            add { _reader.EventRaised += value; }
            remove { _reader.EventRaised -= value; }
        }

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
}