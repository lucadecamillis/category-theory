using Category.Theory.Monads;

namespace Category.Theory.Nullable;

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
    /// Map the given nullable into a maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="nullable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Maybe<TResult> Select<T, TResult>(this Nullable<T> nullable, Func<T, TResult> selector) where T : struct
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

    /// <summary>
    /// Enables query syntaxt for nullable
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="n1"></param>
    /// <param name="n2"></param>
    /// <param name="tresult"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Maybe<TResult> SelectMany<T1, T2, TResult>(
        this Nullable<T1> n1,
        Func<T1, Nullable<T2>> n2,
        Func<T1, T2, TResult> tresult)
        where T1 : struct
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

        if (tresult == null)
        {
            throw new ArgumentNullException(nameof(tresult));
        }

        return n1.SelectMany(x => n2(x).Select(y => tresult(x, y)));
    }

    /// <summary>
    /// Enables query syntaxt for nullable
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="n1"></param>
    /// <param name="n2"></param>
    /// <param name="tresult"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Maybe<TResult> SelectMany<T1, T2, TResult>(
        this Maybe<T1> n1,
        Func<T1, Nullable<T2>> n2,
        Func<T1, T2, TResult> tresult)
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

        if (tresult == null)
        {
            throw new ArgumentNullException(nameof(tresult));
        }

        return n1.SelectMany(x => n2(x).Select(y => tresult(x, y)));
    }

        /// <summary>
    /// Enables query syntaxt for nullable
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="n1"></param>
    /// <param name="n2"></param>
    /// <param name="tresult"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Maybe<TResult> SelectMany<T1, T2, TResult>(
        this Nullable<T1> n1,
        Func<T1, Maybe<T2>> n2,
        Func<T1, T2, TResult> tresult)
        where T1 : struct
    {
        if (n1 == null)
        {
            throw new ArgumentNullException(nameof(n1));
        }

        if (n2 == null)
        {
            throw new ArgumentNullException(nameof(n2));
        }

        if (tresult == null)
        {
            throw new ArgumentNullException(nameof(tresult));
        }

        return n1.SelectMany(x => n2(x).Select(y => tresult(x, y)));
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
}