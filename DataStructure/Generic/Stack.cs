using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.Generic
{
    public class Stack<T> : IStack<T>
    {
        private Node _current;

        public IEnumerator<T> GetEnumerator() 
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, "Index out  of range");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Invalid Off length");
            }
            
            var current = _current;
            for (var i = 0; current != null; i++)
            {
                array[i] = current.Value;
                current = current.Preview;
            }
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, "Index out  of range");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Invalid Off length");
            }

            var source = new object[Count];
            
            var current = _current;
            for (var i = 0; current != null; i++)
            {
                source[i] = current.Value;
                current = current.Preview;
            }

            try
            {
                Array.Copy(source, 0, array, arrayIndex, Count);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("Invalid array type", nameof(array));
            }
        }

        void ICollection<T>.Add(T item) 
            => Push(item);

        public void Clear()
        {
            _current = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var node = _current;
            
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }

                node = node.Preview;
            }

            return false;
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        object IStack.Peek()
            => Peek();
        
        public T Peek()
        {
            if (_current == null)
            {
                throw new InvalidOperationException("Empty stack");
            }
            
            return _current.Value;
        }

        public bool TryPeek(out T result)
        {
            if (_current == null)
            {
                result = default;
                return false;
            }

            result = _current.Value;
            return true;
        }
        
        bool IStack.TryPeek(out object result)
        {
            if (_current == null)
            {
                result = null;
                return false;
            }

            result = _current.Value;
            return true;
        }

        public void Push(T item)
        {
            Count++;
            _current = new Node(_current, item);
        }
        
        void IStack.Push(object item)
        {
            if (item is T t)
            {
                Push(t);
            }
            
            throw new InvalidOperationException();
        }
        
        object IStack.Pop() 
            => Pop();

        public T Pop()
        {
            if (_current == null)
            {
                throw new InvalidOperationException("Empty stack");
            }

            var ret = _current.Value;
            _current = _current.Preview;
            Count--;
            return ret;
        }

        public bool TryPop(out T result)
        {
            if (_current == null)
            {
                result = default;
                return false;
            }

            result = _current.Value;
            _current = _current.Preview;
            Count--;
            return true;
        }

        bool IStack.TryPop(out object result)
        {
            if (_current == null)
            {
                result = default;
                return false;
            }

            result = _current.Value;
            _current = _current.Preview;
            Count--;
            return true;
        }
        
        private class Node
        {
            public Node(Node preview, T value)
            {
                Preview = preview;
                Value = value;
            }

            internal T Value { get; }
            internal Node Preview { get; }
        }

        private struct Enumerator : IEnumerator<T>
        {
            private readonly Stack<T> _stack;
            private bool _isFirst;
            private Node _current;

            public Enumerator(Stack<T> stack)
            {
                _stack = stack;
                _current = null;
                _isFirst = true;
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    if (_isFirst && _stack._current != null)
                    {
                        _current = _stack._current;
                        _isFirst = false;
                        return true;
                    }

                    return false;
                }
                
                _current = _current.Preview;
                return true;
            }

            public void Reset()
            {
                _isFirst = true;
                _current = null;
            }

            object IEnumerator.Current => Current;

            public T Current => _current.Value;
            
            public void Dispose()
            {
            }
        }
    }
}