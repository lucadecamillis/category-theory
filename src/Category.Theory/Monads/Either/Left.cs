using System;

namespace Category.Theory.Monads
{
    internal class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TLeft value;

        public Left(TLeft value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
        }

        public override bool HasLeft()
        {
            return true;
        }

        public override bool HasRight()
        {
            return false;
        }

        public override Either<TLeft, TRight> IfLeft(Action<TLeft> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(this.value);

            return this;
        }

        public override Either<TLeft, TRight> IfRight(Action<TRight> action)
        {
            return this;
        }

        public override bool TryGetLeft(out TLeft leftValue, out TRight rightValue)
        {
            leftValue = this.value;
            rightValue = default;
            return true;
        }

        public override bool TryGetRight(out TRight rightValue, out TLeft leftValue)
        {
            rightValue = default;
            leftValue = this.value;
            return false;
        }

        public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
        {
            return left(this.value);
        }

        public override void Iter(Action<TLeft> leftAction, Action<TRight> rightAction)
        {
            leftAction(this.value);
        }

        public override Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> selector)
        {
            return new Left<TLeft, TResult>(this.value);
        }

        public override Either<TResult, TRight> SelectLeft<TResult>(Func<TLeft, TResult> selector)
        {
            return new Left<TResult, TRight>(selector(this.value));
        }

        public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
        {
            return new Left<TLeft, T1Right>(this.value);
        }

        public override bool EqualsTo(TRight right)
        {
            return false;
        }

        public override bool EqualsTo(TLeft left)
        {
            return Equals(this.value, left);
        }

        public override string ToString()
        {
            return $"Left {this.value}";
        }
    }
}