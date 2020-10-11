using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.SimulatedAnnealing
{
    public class TSP
    {
        public readonly struct Point
        {
            public int X { get; }
            public int Y { get; }
            
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public double GetEuclideanDistance(in Point to)
                => Math.Sqrt(Math.Pow(this.X - to.X, 2) + Math.Pow(this.Y - to.Y, 2));
        }

        public class Result
        {
            public int[] State { get; internal set; }
            public double Energy { get; internal set; }

            public Result(int length)
            {
                if (length < 1)
                    throw new ArgumentException("Must be at least 1", nameof(length));

                State = InitState(length);
            }

            private int[] InitState(int length)
            {
                var array = new int[length];
                for (int i = 0; i < array.Length; i++)
                    array[i] = i;
                
                var random = new Random();
                for (int i = 0; i < array.Length; i++)
                    array.Swap(i, random.Next(array.Length));

                return array;
            }
        }

        private const int DefaultIterationsCount = 10000;
        private const double DefaultTemperatureDecreaseRatio = 0.1;
        
        private readonly Random _random = new Random();
        private readonly int _iterationsCount;
        private readonly double  _temperatureDecreaseRatio;

        public TSP() 
            : this (DefaultIterationsCount, DefaultTemperatureDecreaseRatio) { }

        public TSP(int iterationsCount, double temperatureDecreaseRatio)
        {
            if (iterationsCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(iterationsCount));
            if (temperatureDecreaseRatio <= 0)
                throw new ArgumentException("Must be greater than 0", nameof(temperatureDecreaseRatio));

            _iterationsCount = iterationsCount;
            _temperatureDecreaseRatio = temperatureDecreaseRatio;
        }

        public Result Optimization(Point[] points, double startTemperature, double endTemperature)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException("Must contain at least one element", nameof(points));
            if (startTemperature < endTemperature)
                throw new ArgumentException("Must be less then end startTemperature", nameof(endTemperature));

            var result = new Result(points.Length);
            result.Energy = CalculateEnergy(result.State, points);
            
            var temperature = startTemperature;

            for (int i = 1; i < _iterationsCount; i++)
            {
                GenerateStateCandidate(result.State);

                var candidateState = result.State;
                var candidateEnergy = CalculateEnergy(candidateState, points);

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

        private double CalculateEnergy(int[] state, Point[] points)
        {
            var energy = 0.0;
            for (int i = 0; i < state.Length - 1; i++)
                energy += points[state[i]].GetEuclideanDistance(in points[state[i + 1]]);

            energy += points[state[state.Length - 1]].GetEuclideanDistance(in points[state[0]]); // close the route
            return energy;
        }

        private void GenerateStateCandidate(int[] state)
        {
            var i = _random.Next(0, state.Length);
            var j = _random.Next(1, state.Length);
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
            => i > 0 ? temperature * _temperatureDecreaseRatio / i : temperature;
    }
}