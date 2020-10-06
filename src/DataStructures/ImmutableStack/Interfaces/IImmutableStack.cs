namespace Cnsl.DataStructures
{
    public interface IImmutableStack<T>
    {
        bool IsEmpty { get; }
        IImmutableStack<T> Push(T value);
        IImmutableStack<T> Pop();
        T Peek();
        IImmutableStack<T> Clear();
    }
}