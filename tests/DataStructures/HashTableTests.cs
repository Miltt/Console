using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        public void AddTest()
        {
            var hashTable = new HashTable<int, string>();
            hashTable.Add(1, "a");

            Assert.IsTrue(hashTable.Count == 1, "Wrong number of items added");
        }

        [TestMethod]
        public void KeyExistsTest()
        {
            const int key = 1;

            var hashTable = new HashTable<int, string>();
            hashTable.Add(key, "a");

            Assert.IsTrue(hashTable.ExistsKey(key), "Added item not found");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int keyForRemove = 2;

            var hashTable = new HashTable<int, string>();
            hashTable.Add(1, "a");
            hashTable.Add(keyForRemove, "b");
            hashTable.Remove(keyForRemove);

            Assert.IsTrue(!hashTable.ExistsKey(keyForRemove), "The deleted item exists");
        }
    }
}