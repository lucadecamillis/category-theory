namespace Category.Theory.Monads;

public static class EitherExtensions
{
    public static TRight GetRightOrFallback<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        TRight fallbackValue)
    {
        if (either.TryGetRight(out TRight value))
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
            Left: l => Maybe.None<T>(),
            Right: r => r);
    }
}