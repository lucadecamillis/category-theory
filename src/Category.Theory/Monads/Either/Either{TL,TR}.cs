using System;

namespace Category.Theory.Monads
{
    public abstract class Either<TLeft, TRight>
    {
        /// <summary>
        /// Return whether an object of type <typeparamref name="TLeft"/> is set
        /// </summary>
        /// <returns></returns>
        public abstract bool HasLeft();

        /// <summary>
        /// Return whether an object of type <typeparamref name="TRight"/> is set
        /// </summary>
        /// <returns></returns>
        public abstract bool HasRight();

        public abstract Either<TLeft, TRight> IfLeft(Action<TLeft> action);

        public abstract Either<TLeft, TRight> IfRight(Action<TRight> action);

        public abstract Either<TLeft, TRight> If<T>(Action<TLeft> action);

        public abstract Either<TLeft, TRight> If<T>(Action<TRight> action);

        /// <summary>
        /// If True the left value is returned and the other (right) value is set the its default.
        /// If False values are returned the other way around.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherValue"></param>
        /// <returns></returns>
        public abstract bool TryGetLeft(out TLeft value, out TRight otherValue);

        /// <summary>
        /// If True the right value is returned and the other (left) value is set the its default.
        /// If False values are returned the other way around.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherValue"></param>
        /// <returns></returns>
        public abstract bool TryGetRight(out TRight value, out TLeft otherValue);

        public abstract bool TryGet<T>(out TLeft value, out TRight otherValue);
        public abstract bool TryGet(out TLeft value);
        public abstract bool TryGet(out TRight otherValue);

        public abstract bool Has<T>();


        public abstract Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> selector);

        public abstract Either<TLeft, TResult> SelectMany<TResult>(Func<TRight, Either<TLeft, TResult>> selector);

        public abstract TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right);

        public abstract void Iter(Action<TLeft> leftAction, Action<TRight> rightAction);

        public static implicit operator Either<TLeft, TRight>(TLeft left) => new Left<TLeft, TRight>(left);

        public static implicit operator Either<TLeft, TRight>(TRight right) => new Right<TLeft, TRight>(right);
    }
}