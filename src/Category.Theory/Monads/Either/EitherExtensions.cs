using System;
using System.Collections.Generic;

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

        public static TRight GetRightOrThrow<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TLeft, Exception> exceptionFunc)
        {
            if (either.TryGetRight(out var rightValue, out var leftValue))
            {
                return rightValue;
            }
            else
            {
                throw exceptionFunc(leftValue);
            }
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
        /// Select left values in the list of <see cref="Either{TLeft, TRight}"/>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TLeft> SelectLefts<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> items)
        {
            if (items == null)
            {
                yield break;
            }

            foreach (var item in items)
            {
                if (item.TryGetLeft(out TLeft value, out _))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Select right values in the list of <see cref="Either{TLeft, TRight}"/>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TRight> SelectRights<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> items)
        {
            if (items == null)
            {
                yield break;
            }

            foreach (var item in items)
            {
                if (item.TryGetRight(out TRight value, out _))
                {
                    yield return value;
                }
            }
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

        /// <summary>
        /// Traverse the given list performing the action on each element
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Either<TLeft, IReadOnlyList<TResult>> Traverse<TLeft, TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Either<TLeft, TResult>> selector)
        {
            List<TResult> collection = new List<TResult>();
            foreach (var item in source)
            {
                if (selector(item).TryGetLeft(out TLeft leftValue, out TResult result))
                {
                    // Short circuit
                    return leftValue;
                }
                collection.Add(result);
            }

            return Either.Right<TLeft, IReadOnlyList<TResult>>(collection);
        }
    }
}