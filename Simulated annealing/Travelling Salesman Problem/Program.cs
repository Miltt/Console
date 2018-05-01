using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravellingSalesmanProblem
{
    public sealed class SimulatedAnnealing
    {
        private const int MaxCycles = 10000;
        private const double CoefTemperature = 0.1;
        
        private Random _random = new Random();

        public int[] State { get; private set; }
        public double Energy { get; private set; }

        public void Start(List<City> cities, double startTemperature, double endTemperature)
        {
            State = Helper.CreateArray(cities.Count).OrderBy(x => _random.Next()).ToArray(); // init random state

            var temperature = startTemperature;
            Energy = CalculateEnergy(State, cities); // calculated energy for the first state

            for (var i = 1; i < MaxCycles; i++)
            {
                GenerateStateCandidate(State); // prepare the state of candidate

                var candidateState = State;
                var candidateEnergy = CalculateEnergy(candidateState, cities); // calculated candidate state energy    

                if (candidateEnergy < Energy) // the candidate has less energy
                {
                    Energy = candidateEnergy; // it goes into the current state
                    State = candidateState;
                }
                else
                {
                    var probability = GetTransitionProbability(candidateEnergy - Energy, temperature); // otherwise, calculate the probability
                    if (MakeTransit(probability)) // and look, are there transition
                    {
                        Energy = candidateEnergy;
                        State = candidateState;
                    }
                }

                temperature = startTemperature * CoefTemperature / i; // temperature reduction
                if (temperature <= endTemperature) // check exit conditions
                    break;
            }
        }

        private double CalculateEnergy(int[] state, List<City> cities)
        {
            var energy = 0.0;
            for (var i = 0; i < state.Count() - 1; i++)            
                energy += GetEuclideanDistance(cities.ElementAt(state[i]), cities.ElementAt(state[i + 1]));            

            energy += GetEuclideanDistance(cities.ElementAt(state[state.Count() - 1]), cities.ElementAt(state[0])); // close the route
            return energy;
        }

        private double GetEuclideanDistance(City cityFrom, City cityTo)
        {
            return Math.Sqrt(Math.Pow(cityFrom.X - cityTo.X, 2) + Math.Pow(cityFrom.Y - cityTo.Y, 2));
        }

        private void GenerateStateCandidate(int[] state)
        {
            var i = _random.Next(0, state.Count());
            var j = _random.Next(0, state.Count());
            if (j < i)
                state.Swap(j, i);
            else
                state.Swap(i, j);
        }

        private double GetTransitionProbability(double energy, double temperature)
        {
            return Math.Exp(-energy / temperature);
        }

        private bool MakeTransit(double probability)
        {
            if (probability > 1 || probability < 0)
                throw new ArgumentException(nameof(probability));

            return (_random.Next(0, 1) <= probability);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cities = new List<City>
            {
                new City { X = 500, Y = 500 },
                new City { X = 2000, Y = 250 },
                new City { X = 1000, Y = 2500 },
                new City { X = 1500, Y = 1200 },
                new City { X = 2465, Y = 1779 }
            };
            var startTemperature = 100;
            var endTemperature = 0.000001;

            var annealing = new SimulatedAnnealing();
            annealing.Start(cities, startTemperature, endTemperature);

            var sb = new StringBuilder();
            annealing.State.ForEach(s => sb.Append(s));

            Console.WriteLine($"Route: {sb.ToString()}. Energy: {annealing.Energy}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public class City
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public static class Helper
    {
        public static void Swap(this int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static int[] CreateArray(int length)
        {
            var array = new int[length];
            for (var i = 0; i < array.Length; i++)
                array[i] = i;            
            return array;
        }

        public static void ForEach<T>(this T[] items, Action<T> action)
        {
            if (items == null || !items.Any())
                return;

            foreach (var item in items)
                action?.Invoke(item);
        }
    }
}
