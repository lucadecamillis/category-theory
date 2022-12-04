using System;

namespace Category.Theory.Monads
{
    internal class Some<T> : Maybe<T>, IEquatable<Some<T>>
    {
        readonly T value;

        public Some(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
        }

        public override bool HasValue()
        {
            return true;
        }

        public override bool TryGetValue(out T value)
        {
            value = this.value;
            return true;
        }

        public override bool EqualsTo(T item)
        {
            return Equals(this.value, item);
        }

        public bool Equals(Some<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(other.value, this.value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Some<T> other)
            {
                return this.Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}