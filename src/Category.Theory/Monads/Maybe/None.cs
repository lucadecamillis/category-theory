namespace Category.Theory.Monads;

internal class None<T> : Maybe<T>, IEquatable<None<T>>
{
    internal static Maybe<T> Instance { get; } = new None<T>();

    public override bool HasValue()
    {
        return false;
    }

    public override T GetValueOrThrow(string errorMessage = null)
    {
        throw new InvalidOperationException(errorMessage ?? $"No value set on maybe");
    }

    public override T GetValueOrFallback(T fallbackValue)
    {
        if (fallbackValue == null)
        {
            throw new ArgumentNullException(nameof(fallbackValue));
        }

        return fallbackValue;
    }

    public override Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return None<TResult>.Instance;
    }

    public override Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
    {
        return None<TResult>.Instance;
    }

    public override TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc)
    {
        if (noneFunc == null)
        {
            throw new ArgumentNullException(nameof(noneFunc));
        }

        return noneFunc();
    }

    public override void Iter(Action<T> someAction, Action noneAction)
    {
        if (noneAction == null)
        {
            throw new ArgumentNullException(nameof(noneAction));
        }

        noneAction();
    }

    public override Maybe<T> Where(Func<T, bool> predicate)
    {
        return Instance;
    }

    public override Maybe<T> IfSome(Action<T> action)
    {
        // Nothing to do
        return this;
    }

    public override bool EqualsTo(T item)
    {
        return false;
    }

    public bool Equals(None<T> other)
    {
        return other != null;
    }

    public override bool Equals(object obj)
    {
        if (obj is None<T> other)
        {
            return this.Equals(other);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return "None";
    }
}