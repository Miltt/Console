using System;

namespace Hanoi
{
    public class TowerOfHanoi
    {
        private IPerformer _performer;

        public TowerOfHanoi(IPerformer performer)
        {
            _performer = performer;
        }

        public void Play(int numDisks, int from, int to, int buffer)
        {
            if (numDisks >= 1)
            {
                Play(numDisks - 1, from, buffer, to);
                MoveDisk(from, to);
                Play(numDisks - 1, buffer, to, from);
            }
        }

        private void MoveDisk(int from, int to)
        {
            _performer.Perform(from, to);
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {
            new TowerOfHanoi(new Performer()).Play(3, 1, 3, 2);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(int from, int to);
    }

    public class Performer : IPerformer
    {
        public void Perform(int from, int to)
        {
            Console.WriteLine($"Move top disc from {from} to {to}");
        }
    }
}
