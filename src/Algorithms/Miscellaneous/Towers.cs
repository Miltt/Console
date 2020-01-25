using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cnsl.Algorithms.Miscellaneous
{
    /// <summary>
    /// the Tower of Hanoi is a mathematical game
    /// </summary>
    public class Towers
    {
        public readonly struct Move : IEquatable<Move>
        {
            public int From { get; }
            public int To { get; }
            
            public Move(int from, int to)
            {
                From = from;
                To = to;
            }

            public bool Equals(Move other)
            {
                return From == other.From && To == other.To;
            }

            public override bool Equals(object obj)
            {
                return obj is Move other
                    ? this.Equals(other)
                    : false;
            }

            public override int GetHashCode()
            {
                unchecked 
                {
                    var hashCode = 0;
                    hashCode = (hashCode * 31) ^ From.GetHashCode();
                    hashCode = (hashCode * 31) ^ To.GetHashCode();
                    return hashCode; 
                }
            }

            public override string ToString() 
            {
                return $"{From}-{To}";
            }
        }

        public static IReadOnlyCollection<Move> Run(int diskCount)
        {
            if (diskCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(diskCount));
            
            var result = new List<Move>();
            RunInternal(result, diskCount: diskCount, from: 1, to: diskCount, buffer: 2);
            return result;
        }

        private static void RunInternal(List<Move> moves, int diskCount, int from, int to, int buffer)
        {
            if (diskCount > 0)
            {
                RunInternal(moves, diskCount - 1, from, buffer, to);
                moves.Add(new Move(from, to));
                RunInternal(moves, diskCount - 1, buffer, to, from);
            }
        }
    }
}