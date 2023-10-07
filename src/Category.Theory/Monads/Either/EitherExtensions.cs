using System;

namespace Category.Theory.Monads
{
    public static class EitherExtensions
    {
        public static TRight GetRightOrFallback<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            TRight fallbackValue)
        {
            if (either.TryGetRight(out TRight value, out _))
            {
                return value;
            }

            return fallbackValue;
        }

        public static TLeft GetLeftOrFallback<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            TLeft fallbackValue)
        {
            if (either.TryGetLeft(out TLeft value, out _))
            {
                return value;
            }

            return fallbackValue;
        }

        /// <summary>
        /// Convert the given either to maybe (right value)
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe<TLeft, T>(this Either<TLeft, T> either)
        {
            return either.Match(
                left: l => Maybe.None<T>(),
                right: r => r);
        }

        /// <summary>
        /// Enables query syntax for either
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="tResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Either<TLeft, TResult> SelectMany<TLeft, T1, T2, TResult>(
            this Either<TLeft, T1> t1,
            Func<T1, Either<TLeft, T2>> t2,
            Func<T1, T2, TResult> tResult)
        {
            if (t1 == null)
            {
                throw new ArgumentNullException(nameof(t1));
            }

            if (t2 == null)
            {
                throw new ArgumentNullException(nameof(t2));
            }

            if (tResult == null)
            {
                throw new ArgumentNullException(nameof(tResult));
            }

            return t1.SelectMany(x => t2(x).Select(y => tResult(x, y)));
        }

        /// <summary>
        /// Similar to <see cref="Either{TLeft, TRight}.TryGetLeft"/>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool TryGet<TLeft, TRight>(this Either<TLeft, TRight> either, out TLeft value)
        {
            if (either == null)
            {
                throw new ArgumentNullException(nameof(either));
            }

            return either.TryGetLeft(out value, out _);
        }

        /// <summary>
        /// Similar to <see cref="Either{TLeft, TRight}.TryGetRight"/>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet<TLeft, TRight>(this Either<TLeft, TRight> either, out TRight value)
        {
            if (either == null)
            {
                throw new ArgumentNullException(nameof(either));
            }

            return either.TryGetRight(out value, out _);
        }
    }
}