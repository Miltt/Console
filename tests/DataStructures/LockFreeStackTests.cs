using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class LockFreeStackTests
    {
        [TestMethod]
        public void PushTest()
        {
            var stack = new LockFreeStack<int>();
            stack.Push(2);
            stack.Push(4);

            const int expectedCount = 2;

            Assert.IsTrue(stack.Count == expectedCount, "Incorrect number of items added");
        }

        [TestMethod]
        public void PopTest()
        {
            var stack = new LockFreeStack<int>();
            stack.Push(2);
            stack.Push(4);

            stack.TryPop(out var item);

            const int expectedItem = 4;

            Assert.IsTrue(item == expectedItem, "Incorrect item");
        }

        [TestMethod]
        public void PeekTest()
        {
            var stack = new LockFreeStack<int>();
            stack.Push(2);
            stack.TryPeek(out var item);

            const int expectedItem = 2;

            Assert.IsTrue(expectedItem == item, "Incorrect item");
        }

        [TestMethod]
        public void InitTest()
        {
            var initList = new List<int> { 1, 2, 3 };
            var stack = new LockFreeStack<int>(initList);
            var list = stack.ToList();

            var expectedList = new List<int> { 3, 2, 1 };

            Assert.IsTrue(list.SequenceEqual(expectedList), "Two sequences are not equal by comparing the element");
        }

        [TestMethod]
        public void ToListTest()
        {
            var stack = new LockFreeStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var list = stack.ToList();
            var expectedList = new List<int> { 3, 2, 1 };

            Assert.IsTrue(list.SequenceEqual(expectedList), "Two sequences are not equal by comparing the element");
        }

        [TestMethod]
        public void ClearTest()
        {
            var stack = new LockFreeStack<int>();
            stack.Push(2);
            stack.Clear();

            const int expectedCount = 0;

            Assert.IsTrue(stack.Count == expectedCount, "Incorrect number of items");
        }

        [TestMethod]
        public void MultiThreadPushTest()
        {
            const int threadCount = 10;
            const int itemCount = 5;

            var stack = new LockFreeStack<int>();

            var threads = new Thread[threadCount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => 
                {
                    for (int j = 0; j < itemCount; j++)
                        stack.Push(j);
                });
            }

            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
            for (int i = 0; i < threads.Length; i++)
                threads[i].Join();

            var list = stack.ToList();
            var expectedCount = threadCount * itemCount;

            Assert.IsTrue(list.Count == expectedCount, "Incorrect number of items added");
        }

        [TestMethod]
        public void MultiThreadPopTest()
        {
            const int threadCount = 10;
            const int itemCount = 100;
            
            var collection = new ConcurrentBag<int>();
            var stack = new LockFreeStack<int>(Enumerable.Range(0, itemCount));

            var threads = new Thread[threadCount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => 
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (stack.TryPop(out var item))
                            collection.Add(item);
                    }
                });
            }

            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
            for (int i = 0; i < threads.Length; i++)
                threads[i].Join();

            Assert.IsTrue(collection.Count == itemCount, "Incorrect number of items");
        }
    }
}