using System;

namespace Category.Theory.Monads
{
    internal class Right<TLeft, TRight> : Either<TLeft, TRight>, IEquatable<Right<TLeft, TRight>>
    {
        private readonly TRight value;

        public Right(TRight value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
        }

        public override Either<TLeft, TRight> IfLeft(Action<TLeft> action)
        {
            return this;
        }

        public override Either<TLeft, TRight> IfRight(Action<TRight> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(this.value);

            return this;
        }

        public override Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> selector)
        {
            return new Right<TLeft, TResult>(selector(this.value));
        }

        public override Either<TResult, TRight> SelectLeft<TResult>(Func<TLeft, TResult> selector)
        {
            return new Right<TResult, TRight>(this.value);
        }

        public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
        {
            return selector(this.value);
        }

        public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
        {
            return right(this.value);
        }

        public override void Iter(Action<TLeft> leftAction, Action<TRight> rightAction)
        {
            rightAction(this.value);
        }

        public override bool HasLeft()
        {
            return false;
        }

        public override bool HasRight()
        {
            return true;
        }

        public override bool TryGetLeft(out TLeft leftValue, out TRight rightValue)
        {
            leftValue = default(TLeft);
            rightValue = this.value;
            return false;
        }

        public override bool TryGetRight(out TRight rightValue, out TLeft leftValue)
        {
            rightValue = this.value;
            leftValue = default(TLeft);
            return true;
        }

        public override bool EqualsTo(TRight right)
        {
            return Equals(this.value, right);
        }

        public override bool EqualsTo(TLeft left)
        {
            return false;
        }

        public bool Equals(Right<TLeft, TRight> other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(this.value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Right<TLeft, TRight> other)
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