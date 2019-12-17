using System;
using System.Diagnostics;
using System.Threading;
using Cnsl.Algorithms.Multithreading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Multithreading
{
    [TestClass]
    public class DiningPhilosophersProblemTests
    {
        private long _philosopherFinishedCount = 0;

        [TestMethod]
        public void PhilosopherFinishedEatingTest()
        {
            var timeOut = TimeSpan.FromSeconds(2);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var table = new DiningTable(philosopherCount: 5))
            {
                table.PhilosopherEventRaised += OnPhilosopherEventRaised;
                
                while (Interlocked.Read(ref _philosopherFinishedCount) < 0 || stopWatch.Elapsed < timeOut);
                
                stopWatch.Stop();
            }

            Assert.IsTrue(_philosopherFinishedCount > 0, $"No philosopher has finished eating for {timeOut}");
        }

        private void OnPhilosopherEventRaised(object sender, PhilosopherEventArgs e)
        {
            if (e != null && e.Type == Philosopher.EventType.ReleaseRightFork)
                Interlocked.Increment(ref _philosopherFinishedCount);
        }
    }
}