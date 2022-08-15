namespace Category.Theory.Monads;

public sealed class Maybe<T> : Maybe
{
    internal static Maybe<T> None { get; } = new Maybe<T>();

    internal static Maybe<T> Some(T value)
    {
        return new Maybe<T>(value);
    }

    readonly bool hasValue;

    readonly T value;

    private Maybe()
    {
        this.hasValue = false;
    }

    private Maybe(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.hasValue = true;
        this.value = value;
    }

    public bool HasValue()
    {
        return this.hasValue;
    }

    public T GetValueOrThrow(string errorMessage = null)
    {
        if (!this.HasValue())
        {
            throw new InvalidOperationException(errorMessage ?? $"No value set on maybe");
        }

        return this.value;
    }

    public T GetValueOrFallback(T fallbackValue)
    {
        if (fallbackValue == null)
        {
            throw new ArgumentNullException(nameof(fallbackValue));
        }

        if (this.HasValue())
        {
            return this.value;
        }
        else
        {
            return fallbackValue;
        }
    }

    public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasValue())
        {
            TResult result = selector(this.value);
            if (result != null)
            {
                return Some(result);
            }
        }

        return None<TResult>();
    }

    public Maybe<TResult> Select<TResult>(Func<T, TResult?> selector) where TResult : struct
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasValue())
        {
            TResult? result = selector(this.value);
            if (result.HasValue)
            {
                return Some(result.Value);
            }
        }

        return None<TResult>();
    }

    public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasValue())
        {
            return selector(this.value);
        }
        else
        {
            return None<TResult>();
        }
    }

    public Maybe<TResult> SelectMany<TResult, TCollection>(
        Func<T, Maybe<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector)
    {
        if (collectionSelector == null)
        {
            throw new ArgumentNullException(nameof(collectionSelector));
        }

        if (resultSelector == null)
        {
            throw new ArgumentNullException(nameof(resultSelector));
        }

        if (!this.HasValue())
        {
            return None<TResult>();
        }

        Maybe<TCollection> subElement = collectionSelector(this.value);
        if (!subElement.HasValue())
        {
            return None<TResult>();
        }

        return Some(resultSelector(this.value, subElement.value));
    }

    public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc)
    {
        if (this.HasValue())
        {
            return someFunc(this.value);
        }
        else
        {
            return noneFunc();
        }
    }

    public Maybe<T> Where(Func<T, bool> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (this.HasValue() && predicate(this.value))
        {
            return this;
        }
        else
        {
            return None<T>();
        }
    }

    public Maybe<TResult> OfType<TResult>()
    {
        if (this.HasValue())
        {
            return OfType<TResult>(this.value);
        }

        return None<TResult>();
    }

    public void Exec(Action<T> action)
    {
        if (this.HasValue())
        {
            action?.Invoke(this.value);
        }
    }

    public bool EqualsTo(T item)
    {
        return this.HasValue() && Equals(this.value, item);
    }

    public override bool Equals(object obj)
    {
        if (obj is Maybe<T> other)
        {
            return object.Equals(this.value, other.value);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.HasValue() ? this.value.GetHashCode() : 0;
    }

    public override string ToString()
    {
        return this.HasValue() ? this.value.ToString() : nameof(None);
    }
}

public class Maybe
{
    protected Maybe()
    {

    }

    public static Maybe<T> Some<T>(T item)
    {
        return Maybe<T>.Some(item);
    }

    public static Maybe<T> None<T>()
    {
        return Maybe<T>.None;
    }

    /// <summary>
    /// Create a <see cref="Maybe{T}"/> object checking for null values of the given object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public static Maybe<T> CheckNull<T>(T item) where T : class
    {
        if (item is null)
        {
            return Maybe<T>.None;
        }

        return Some(item);
    }

    /// <summary>
    /// Create a <see cref="Maybe{T}"/> object checking whether the given object is of type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="o"></param>
    /// <returns></returns>
    public static Maybe<T> OfType<T>(object o)
    {
        if (o is T t)
        {
            return Some(t);
        }

        return None<T>();
    }
}