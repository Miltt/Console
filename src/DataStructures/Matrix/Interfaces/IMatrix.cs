namespace Cnsl.DataStructures
{
    public interface IMatrix
    {
        int RowsCount { get; }
        int ColumnsCount { get; }

        int this[int i, int j] { get; set; }
    }
}