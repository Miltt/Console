using System;
using System.Diagnostics;
using System.Threading;
using Cnsl.Algorithms.Multithreading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Multithreading
{
    [TestClass]
    public class ReadersWritersProblemTests
    {
        private long _writerFinishedCount = 0;
        private long _readerFinishedCount = 0;

        [TestMethod]
        public void WriterTest()
        {
            var timeOut = TimeSpan.FromSeconds(4);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var problem = new ReadersWritersProblem(readersCount: 4))
            {
                problem.WriterEventRaised += OnWriterEventRaised;
                
                while (Interlocked.Read(ref _writerFinishedCount) < 0 || stopWatch.Elapsed < timeOut);
                
                problem.WriterEventRaised -= OnWriterEventRaised;
                stopWatch.Stop();
            }

            Assert.IsTrue(_writerFinishedCount > 0, $"The writer did not finish writing for {timeOut}");
        }

        private void OnWriterEventRaised(object sender, WriterEventArgs e)
        {
            if (e != null && e.Type == Writer.EventType.Sleep)
                Interlocked.Increment(ref _writerFinishedCount);
        }

        [TestMethod]
        public void ReadersTest()
        {
            var timeOut = TimeSpan.FromSeconds(2);
            const int readersCount = 4;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var problem = new ReadersWritersProblem(readersCount))
            {
                problem.ReaderEventRaised += OnReaderEventRaised;
                
                while (Interlocked.Read(ref _readerFinishedCount) < readersCount || stopWatch.Elapsed < timeOut);
                
                problem.ReaderEventRaised -= OnReaderEventRaised;
                stopWatch.Stop();
            }

            Assert.IsTrue(_readerFinishedCount >= readersCount, $"The readers are not finished reading for {timeOut}");
        }

        private void OnReaderEventRaised(object sender, ReaderEventArgs e)
        {
            if (e != null && e.Type == Reader.EventType.Read)
                Interlocked.Increment(ref _readerFinishedCount);
        }
    }
}