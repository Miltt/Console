namespace Cnsl.Algorithms.Miscellaneous
{
    public class EightQueensPuzzle
    {
        public static bool TryFind(int boardSize, out Сhessboard board)
        {
            board = new Сhessboard(boardSize);
            return TryFindInternal(in board, 0);
        }

        private static bool TryFindInternal(in Сhessboard board, int i)
        {
            for (int j = 0; j < board.Size; j++)
            {
                if (IsValid(in board, i, j))
                {
                    board[i, j] = true;
                    
                    if (i == board.Size - 1 || TryFindInternal(in board, i + 1))
                        return true;

                    board[i, j] = false;
                }
            }

            return false;
        }

        private static bool IsValid(in Сhessboard board, int i, int j)
        {
            for (int k = 0; k <= i; k++)
            {
                if ((board[k, j]) || 
                    (k <= j && board[i - k, j - k]) || 
                    (j + k < board.Size && board[i - k, j + k]))
                    return false;
            }

            return true;
        }
    }
}