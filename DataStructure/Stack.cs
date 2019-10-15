using System;
using System.Collections;

namespace DataStructure
{
    public class Stack : IStack
    {
        private Node _current;
        
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index out  of range");
            }

            if (array.Length - index < Count)
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
                Array.Copy(source, 0, array, index, Count);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("Invalid array type", nameof(array));
            }
        }

        public int Count { get; private set; }

        public bool IsSynchronized => false;

        public object SyncRoot => this;
        
        public object Peek()
        {
            if (_current == null)
            {
                throw new InvalidOperationException("Empty stack");
            }
            
            return _current.Value;
        }

        public bool TryPeek(out object result)
        {
            if (_current == null)
            {
                result = null;
                return false;
            }

            result = _current.Value;
            return true;
        }

        public void Push(object item)
        {
            Count++;
            _current = new Node(_current, item);
        }

        public object Pop()
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

        public bool TryPop(out object result)
        {
            if (_current == null)
            {
                result = null;
                return false;
            }

            result = _current.Value;
            _current = _current.Preview;
            Count--;
            return true;
        }
        
        private class Node
        {
            public Node(Node preview, object value)
            {
                Preview = preview;
                Value = value;
            }

            internal object Value { get; }
            internal Node Preview { get; }
        }

        private struct Enumerator : IEnumerator
        {
            private readonly Stack _stack;
            private bool _isFirst;
            private Node _current;

            public Enumerator(Stack stack)
            {
                _stack = stack;
                _isFirst = true;
                _current = null;
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

            public object Current => _current.Value;
        }
    }
}