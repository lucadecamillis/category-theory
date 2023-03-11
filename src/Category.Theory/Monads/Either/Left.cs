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

        public override bool TryGetLeft(out TLeft value, out TRight otherValue)
        {
            value = this.value;
            otherValue = default;
            return true;
        }

        public override bool TryGetRight(out TRight value, out TLeft otherValue)
        {
            value = default;
            otherValue = this.value;
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

        public override Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector)
        {
            return new Left<TLeft, T1Right>(this.value);
        }

        public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
        {
            return new Left<TLeft, T1Right>(this.value);
        }
    }
}