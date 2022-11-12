namespace Category.Theory.Monads;

internal class Some<T> : Maybe<T>, IEquatable<Some<T>>
{
    readonly T value;

    public Some(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.value = value;
    }

    public override bool HasValue()
    {
        return true;
    }

    public override T GetValueOrThrow(Exception ex)
    {
        return this.value;
    }

    public override T GetValueOrThrow(string errorMessage = null)
    {
        return this.value;
    }

    public override T GetValueOrFallback(T fallbackValue)
    {
        return this.value;
    }

    public override bool TryGetValue(out T value)
    {
        value = this.value;
        return true;
    }

    public override Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        TResult result = selector(this.value);
        if (result != null)
        {
            return new Some<TResult>(result);
        }

        return None<TResult>.Instance;
    }

    public override Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        return selector(this.value);
    }

    public override TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc)
    {
        if (someFunc == null)
        {
            throw new ArgumentNullException(nameof(someFunc));
        }

        return someFunc(this.value);
    }

    public override void Iter(Action<T> someAction, Action noneAction)
    {
        if (someAction == null)
        {
            throw new ArgumentNullException(nameof(someAction));
        }

        someAction(this.value);
    }

    public override Maybe<T> Where(Func<T, bool> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (predicate(this.value))
        {
            return new Some<T>(this.value);
        }

        return None<T>.Instance;
    }

    public override Maybe<T> IfSome(Action<T> action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        action.Invoke(this.value);

        return this;
    }

    public override bool EqualsTo(T item)
    {
        return Equals(this.value, item);
    }

    public bool Equals(Some<T> other)
    {
        if (other == null)
        {
            return false;
        }

        return Equals(other.value, this.value);
    }

    public override bool Equals(object obj)
    {
        if (obj is Some<T> other)
        {
            return this.Equals(other);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.value.GetHashCode();
    }

    public override string ToString()
    {
        return this.value.ToString();
    }
}