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

        public override bool TryGetLeft(out TLeft value, out TRight otherValue)
        {
            value = default!;
            otherValue = this.value;
            return false;
        }

        public override bool TryGetRight(out TRight value, out TLeft otherValue)
        {
            value = this.value;
            otherValue = default!;
            return true;
        }

        public override bool TryGet<T>(out TLeft value, out TRight otherValue)
        {
            value = default!;
            if (typeof(T) == typeof(TRight))
            {
                otherValue = this.value;
                return true;
            }
            else
            {
                otherValue = this.value;
                return false;
            }
        }

        public override bool TryGet(out TLeft value)
        {
            value = default!;
            return false;
        }

        public override bool TryGet(out TRight value)
        {
            value = this.value;
            return true;
        }

        public override bool Has<T>()
        {
            return typeof(T) == typeof(TRight);
        }

        public override Either<TLeft, TRight> If<T>(Action<TLeft> action)
        {
            return this;
        }

        public override Either<TLeft, TRight> If<T>(Action<TRight> action)
        {
            if (typeof(T) == typeof(TRight))
            {
                action(this.value);
            }

            return this;
        }
    }
}