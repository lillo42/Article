using System.Collections.Generic;

namespace DataStructure.Generic
{
    public interface IStack<T> : IStack, ICollection<T>, IReadOnlyCollection<T>
    {
        new T Peek();
        bool TryPeek(out T result);
        
        void Push(T item);
        new T Pop();
        bool TryPop(out T result);
    }
}