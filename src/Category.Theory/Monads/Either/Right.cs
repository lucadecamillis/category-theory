using System;

namespace Category.Theory.Monads
{
    internal class Right<TLeft, TRight> : Either<TLeft, TRight>
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

        public override Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector)
        {
            return new Right<TLeft, T1Right>(selector(this.value));
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
            leftValue = default;
            rightValue = this.value;
            return false;
        }

        public override bool TryGetRight(out TRight rightValue, out TLeft leftValue)
        {
            rightValue = this.value;
            leftValue = default;
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
    }
}