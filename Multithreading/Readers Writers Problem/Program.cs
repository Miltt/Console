using System;
using System.Threading;

namespace ReadersWriters
{
    public class Writer
    {
        private const int PrepareTimeout = 1000;
        private const int WriteTimeout = 2000;

        private bool _canWrite = true;
        private object _sync = new object();

        public void Lock()
        {
            lock (_sync)
            {
                if (!_canWrite)
                    Monitor.Wait(_sync);

                _canWrite = false;
                Log.Add("Lock");
            }
        }

        public void Unlock()
        {
            lock (_sync)
            {
                _canWrite = true;
                Log.Add("Unlock");
                Monitor.Pulse(_sync);                
            }            
        }

        public void Prepare()
        {
            Log.Add("Prepare...");
            Thread.Sleep(PrepareTimeout);
        }

        public void Write()
        {
            Log.Add("Write...");
            Thread.Sleep(WriteTimeout);            
        }
    }

    public class Reader
    {
        private Writer _writer;
        private object _sync = new object();
        private int _numReaders;        

        public Reader(Writer writer)
        {
            _writer = writer;
        }

        public void Lock()
        {
            lock (_sync)
            {
                _numReaders++;
                if (_numReaders == 1)
                    _writer.Lock();
            }
        }

        public void Unlock()
        {
            lock (_sync)
            {
                _numReaders--;
                if (_numReaders == 0)
                    _writer.Unlock();
            }
        }

        public void Read()
        {
            Log.Add("Read...");
            Thread.Sleep(2000);
        }

        public void Use()
        {
            Log.Add("Use...");
            Thread.Sleep(3000);
        }
    }

    public sealed class Sync : IDisposable
    {        
        private Writer _writer;
        private Reader _reader;
        private int _numReaders;
        private bool _disposed;

        public Sync(int numReaders)
        {
            _numReaders = numReaders;

            _writer = new Writer();
            _reader = new Reader(_writer);
        }

        public void Start()
        {
            var threadWriter = new Thread(() =>
            {
                while (!_disposed)
                {                
                    _writer.Prepare();
                    _writer.Lock();
                    _writer.Write();
                    _writer.Unlock();
                }
            });
            threadWriter.Name = "Writer 0";
            threadWriter.Start();

            for (var i = 0; i < _numReaders; i++)
            {
                var threadReader = new Thread(() =>
                {
                    while (!_disposed)
                    {
                        _reader.Lock();
                        _reader.Read();
                        _reader.Unlock();
                        _reader.Use();
                    }
                });
                threadReader.Name = "Reader " + i.ToString();
                threadReader.Start();
            }
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
            var numReaders = 2;
            using (var s = new Sync(numReaders))
            {
                s.Start();
                Console.ReadKey();
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }    
    }

    public static class Log
    {
        public static void Add(string text)
        {
            Console.WriteLine(string.Format("{0}\t : {1}", Thread.CurrentThread.Name, text));
        }
    }
}
