using Category.Theory.Monads;

namespace Category.Theory.SumTypes;

public static class OneOfT2Extensions
{
    public static Maybe<T0> TryGetT0<T0, T1, T2>(this OneOf<T0, T1, T2> oneOf)
    {
        if (oneOf.TryGetT0(out T0 value))
        {
            return value;
        }

        return Maybe.None<T0>();
    }

    public static Maybe<T1> TryGetT1<T0, T1, T2>(this OneOf<T0, T1, T2> oneOf)
    {
        if (oneOf.TryGetT1(out T1 value))
        {
            return value;
        }

        return Maybe.None<T1>();
    }

    public static Maybe<T2> TryGetT2<T0, T1, T2>(this OneOf<T0, T1, T2> oneOf)
    {
        if (oneOf.TryGetT2(out T2 value))
        {
            return value;
        }

        return Maybe.None<T2>();
    }
}