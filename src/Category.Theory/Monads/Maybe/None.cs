using System;

namespace Category.Theory.Monads
{
    internal class None<T> : Maybe<T>, IEquatable<None<T>>
    {
        internal static Maybe<T> Instance { get; } = new None<T>();

        private None()
        {
            
        }

        public override bool HasValue()
        {
            return false;
        }

        public override bool Empty()
        {
            return true;
        }

        public override bool TryGetValue(out T value)
        {
            value = default(T);
            return false;
        }

        public override bool EqualsTo(T item)
        {
            return false;
        }

        public bool Equals(None<T> other)
        {
            return other != null;
        }

        public override bool Equals(object obj)
        {
            if (obj is None<T> other)
            {
                return this.Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "None";
        }
    }
}