using System.Collections;

namespace DataStructure
{
    public interface IStack : ICollection
    { 
        object Peek();
        bool TryPeek(out object result);
        
        void Push(object item);
        object Pop();
        bool TryPop(out object result);
    }
} 
