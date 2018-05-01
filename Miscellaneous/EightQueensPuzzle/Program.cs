using System;

namespace EQP
{
    public class EightQueensPuzzle
    {
        private const int N = 8;

        public void Solve()
        {
            var board = new bool[N, N];
            if (CanFind(board, 0))
                Show(board);
        }

        private bool Allowed(bool[,] board, int x, int y)
        {
            for (int i = 0; i <= x; i++)
            {
                if (board[i, y] || (i <= y && board[x - i, y - i]) || (y + i < N && board[x - i, y + i]))
                    return false;
            }

            return true;
        }

        private bool CanFind(bool[,] board, int x)
        {
            for (var y = 0; y < N; y++)
            {
                if (Allowed(board, x, y))
                {
                    board[x, y] = true;
                    if (x == N - 1 || CanFind(board, x + 1))
                        return true;

                    board[x, y] = false;
                }
            }

            return false;
        }

        private void Show(bool[,] board)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(board[i, j] ? i + ":" + j + " " : "");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new EightQueensPuzzle().Solve();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}