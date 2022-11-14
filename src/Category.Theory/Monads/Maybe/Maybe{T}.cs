namespace Category.Theory.Monads;

public abstract class Maybe<T>
{
    protected Maybe()
    {

    }

    public abstract bool HasValue();

    public abstract bool TryGetValue(out T value);

    public abstract bool EqualsTo(T item);

    public Maybe<TResult> OfType<TResult>()
    {
        if (TryGetValue(out T value) && value is TResult t)
        {
            return t;
        }

        return None<TResult>.Instance;
    }

    public static implicit operator Maybe<T>(T value) => new Some<T>(value);

    public static implicit operator Maybe<T>(Types.None _) => new None<T>();
}