namespace Cnsl.Common.Collections
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