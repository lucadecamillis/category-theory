using System;

namespace Category.Theory.Monads
{
    public abstract class Either<TLeft, TRight>
    {
        public abstract bool HasLeft();

        public abstract bool HasRight();

        public abstract Either<TLeft, TRight> IfLeft(Action<TLeft> action);

        public abstract Either<TLeft, TRight> IfRight(Action<TRight> action);

        public abstract bool TryGetLeft(out TLeft value);

        public abstract bool TryGetRight(out TRight value);

        public abstract Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector);

        public abstract Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector);

        public abstract TResult Match<TResult>(Func<TLeft, TResult> Left, Func<TRight, TResult> Right);

        public static implicit operator Either<TLeft, TRight>(TLeft left) => new Left<TLeft, TRight>(left);

        public static implicit operator Either<TLeft, TRight>(TRight right) => new Right<TLeft, TRight>(right);
    }
}