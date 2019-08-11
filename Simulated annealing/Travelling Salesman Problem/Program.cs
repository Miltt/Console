using System;

namespace TravellingSalesmanProblem
{
    public struct SimulatedAnnealingResult
    {
        public int[] State;
        public double Energy;
    }

    public sealed class SimulatedAnnealing
    {
        private const int DefaultIterationsCount = 10000;
        private const double TemperatureDecreaseRatio = 0.1;
        
        private readonly Random _random = new Random();

        public SimulatedAnnealingResult Start(City[] cities, double startTemperature, double endTemperature)
        {
            if (cities == null || cities.Length == 0)
                throw new ArgumentException("Must contain at least one element", nameof(cities));
            if (startTemperature < endTemperature)
                throw new ArgumentException("Must be less then end startTemperature", nameof(endTemperature));

            var result = new SimulatedAnnealingResult();
            result.State = CreateArray(cities.Length);
            result.Energy = CalculateEnergy(result.State, cities);
            
            var temperature = startTemperature;            

            for (var i = 1; i < DefaultIterationsCount; i++)
            {
                GenerateStateCandidate(result.State);

                var candidateState = result.State;
                var candidateEnergy = CalculateEnergy(candidateState, cities);

                if (candidateEnergy < result.Energy || 
                    MakeTransit(GetTransitionProbability(candidateEnergy - result.Energy, temperature)))
                {
                    result.Energy = candidateEnergy;
                    result.State = candidateState;
                }

                temperature = DecreaseTemperature(startTemperature, i);
                if (temperature <= endTemperature)
                    break;
            }

            return result;
        }

        private double CalculateEnergy(int[] state, City[] cities)
        {
            var energy = 0.0;
            for (int i = 0; i < state.Length - 1; i++)            
                energy += GetEuclideanDistance(cities[state[i]], cities[state[i + 1]]);

            energy += GetEuclideanDistance(cities[state[state.Length - 1]], cities[state[0]]); // close the route
            return energy;
        }

        private double GetEuclideanDistance(City from, City to)
            => Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));

        private void GenerateStateCandidate(int[] state)
        {
            var i = _random.Next(0, state.Length);
            var j = _random.Next(0, state.Length);
            if (j < i)
                state.Swap(j, i);
            else
                state.Swap(i, j);
        }

        private double GetTransitionProbability(double energy, double temperature)
            => Math.Exp(-energy / temperature);

        private bool MakeTransit(double probability)
            => probability > 1 || probability < 0 ? false : _random.NextDouble() <= probability;

        private double DecreaseTemperature(double temperature, int i)
            => i > 0 ? temperature * TemperatureDecreaseRatio / i : temperature;

        private int[] CreateArray(int length)
        {
            var array = new int[length];
            for (int i = 0; i < array.Length; i++)
                array[i] = i;            
            
            for (int i = 0; i < array.Length; i++)
                array.Swap(i, _random.Next(array.Length));

            return array;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cities = new City[]
            {
                new City(500, 500),
                new City(2000, 250),
                new City(1000, 2500),
                new City(1500, 1200),
                new City(2465, 1779)
            };

            var result = new SimulatedAnnealing().Start(cities, startTemperature: 100, endTemperature: 0.000001);
            
            Console.Write($"Energy: {result.Energy}, Route: ");        
            for (int i = 0; i < result.State.Length; i++)
                Console.Write($"{result.State[i]} ");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }    

    public struct City
    {
        public int X;
        public int Y;
        
        public City(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static class Extension
    {
        public static void Swap(this int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}