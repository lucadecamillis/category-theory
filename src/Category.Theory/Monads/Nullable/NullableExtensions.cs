using System;
using Category.Theory.Monads;

namespace Category.Theory.Nullable
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Return the value (if present) or a provided fallback value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="fallbackValue"></param>
        /// <returns></returns>
        public static T GetValueOrFallback<T>(this Nullable<T> nullable, T fallbackValue) where T : struct
        {
            if (nullable.HasValue)
            {
                return nullable.Value;
            }

            return fallbackValue;
        }

        /// <summary>
        /// Map the given nullable into a nullable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Nullable<TResult> Select<T, TResult>(this Nullable<T> nullable, Func<T, TResult> selector)
            where T : struct
            where TResult : struct
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (nullable.HasValue)
            {
                return selector(nullable.Value);
            }

            return null;
        }

        public static Maybe<TResult> SelectMany<T, TResult>(this Nullable<T> nullable, Func<T, Maybe<TResult>> selector)
            where T : struct
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (nullable.HasValue)
            {
                return selector(nullable.Value);
            }

            return Maybe.None<TResult>();
        }

        public static Nullable<TResult> SelectMany<T, TResult>(this Nullable<T> nullable, Func<T, TResult?> selector)
            where T : struct
            where TResult : struct
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (nullable.HasValue)
            {
                return selector(nullable.Value);
            }

            return null;
        }

        /// <summary>
        /// Enables query syntax for nullable
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="tResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Maybe<TResult> SelectMany<T1, T2, TResult>(
            this Nullable<T1> n1,
            Func<T1, Nullable<T2>> n2,
            Func<T1, T2, TResult> tResult)
            where T1 : struct
            where T2 : struct
        {
            if (n2 == null)
            {
                throw new ArgumentNullException(nameof(n2));
            }

            if (tResult == null)
            {
                throw new ArgumentNullException(nameof(tResult));
            }

            return n1.SelectMany(x => n2(x).ToMaybe(y => tResult(x, y)));
        }

        /// <summary>
        /// Enables query syntax for nullable
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="tResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Maybe<TResult> SelectMany<T1, T2, TResult>(
            this Maybe<T1> n1,
            Func<T1, Nullable<T2>> n2,
            Func<T1, T2, TResult> tResult)
            where T2 : struct
        {
            if (n1 == null)
            {
                throw new ArgumentNullException(nameof(n1));
            }

            if (n2 == null)
            {
                throw new ArgumentNullException(nameof(n2));
            }

            if (tResult == null)
            {
                throw new ArgumentNullException(nameof(tResult));
            }

            return n1.SelectMany(x => n2(x).ToMaybe(y => tResult(x, y)));
        }

        /// <summary>
        /// Enables query syntax for nullable
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="tResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Maybe<TResult> SelectMany<T1, T2, TResult>(
            this Nullable<T1> n1,
            Func<T1, Maybe<T2>> n2,
            Func<T1, T2, TResult> tResult)
            where T1 : struct
        {
            if (n2 == null)
            {
                throw new ArgumentNullException(nameof(n2));
            }

            if (tResult == null)
            {
                throw new ArgumentNullException(nameof(tResult));
            }

            return n1.SelectMany(x => n2(x).Select(y => tResult(x, y)));
        }

        /// <summary>
        /// Convert the given nullable into maybe (for value types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct
        {
            if (nullable.HasValue)
            {
                return Maybe.Some(nullable.Value);
            }

            return Maybe.None<T>();
        }

        /// <summary>
        /// Map the given nullable into a maybe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Maybe<TResult> ToMaybe<T, TResult>(this Nullable<T> nullable, Func<T, TResult> selector) where T : struct
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (nullable.HasValue)
            {
                TResult result = selector(nullable.Value);
                if (result != null)
                {
                    return Maybe.Some(result);
                }
            }

            return Maybe.None<TResult>();
        }

        public static void IfSome<T>(this Nullable<T> nullable, Action<T> action) where T : struct
        {
            if (nullable.HasValue)
            {
                action?.Invoke(nullable.Value);
            }
        }

        /// <summary>
        /// Check whether the given nullable entity is equal to the given value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool EqualsTo<T>(this Nullable<T> nullable, T item) where T : struct
        {
            return nullable.HasValue && nullable.Value.Equals(item);
        }
    }
}