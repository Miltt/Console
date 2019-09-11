using System;
using System.Text;

namespace EQP
{
    public struct Сhessboard
    {
        public bool[,] Grid;
        public int Size;

        public Сhessboard(int size)
        {
            Grid = new bool[size, size];
            Size = size;
        }

        public string GetResult()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Grid[i, j])
                        sb.Append(i).Append(':').Append(j).Append(' ');
                }
            }

            return sb.ToString();
        }
    }

    public sealed class EightQueensPuzzle
    {
        public Сhessboard Run(int boardSize)
        {
            if (boardSize <= 0)
                throw new ArgumentException("Must be less than 0", nameof(boardSize));

            var board = new Сhessboard(boardSize);
            
            TryFind(board, 0);

            return board;
        }

        private bool TryFind(in Сhessboard board, int i)
        {
            for (int j = 0; j < board.Size; j++)
            {
                if (IsValid(board, i, j))
                {
                    board.Grid[i, j] = true;
                    
                    if (i == board.Size - 1 || TryFind(board, i + 1))
                        return true;

                    board.Grid[i, j] = false;
                }
            }

            return false;
        }

        private bool IsValid(in Сhessboard board, int i, int j)
        {
            for (int k = 0; k <= i; k++)
            {
                if ((board.Grid[k, j]) || 
                    (k <= j && board.Grid[i - k, j - k]) || 
                    (j + k < board.Size && board.Grid[i - k, j + k]))
                    return false;
            }

            return true;
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var board = new EightQueensPuzzle().Run(boardSize: 8);
            Console.WriteLine(board.GetResult());

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}