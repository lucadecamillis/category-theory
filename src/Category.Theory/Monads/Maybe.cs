namespace Category.Theory.Monads;

public sealed class Maybe<T> : Maybe
{
    internal static Maybe<T> Empty { get; } = new Maybe<T>();

    public bool HasItem { get; }

    public T Item { get; }

    private Maybe()
    {
        this.HasItem = false;
    }

    internal Maybe(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        this.HasItem = true;
        this.Item = item;
    }

    public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasItem)
        {
            TResult result = selector(this.Item);
            if (result != null)
            {
                return FromItem(result);
            }
        }

        return Empty<TResult>();
    }

    public Maybe<TResult> Select<TResult>(Func<T, TResult?> selector) where TResult : struct
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasItem)
        {
            TResult? result = selector(this.Item);
            if (result.HasValue)
            {
                return FromItem(result.Value);
            }
        }

        return Empty<TResult>();
    }

    public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (this.HasItem)
        {
            return selector(this.Item);
        }
        else
        {
            return Empty<TResult>();
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

        if (!this.HasItem)
        {
            return Empty<TResult>();
        }

        Maybe<TCollection> subElement = collectionSelector(this.Item);
        if (!subElement.HasItem)
        {
            return Empty<TResult>();
        }

        return FromItem(resultSelector(this.Item, subElement.Item));
    }

    public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc)
    {
        if (this.HasItem)
        {
            return someFunc(this.Item);
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

        if (this.HasItem && predicate(this.Item))
        {
            return this;
        }
        else
        {
            return Empty<T>();
        }
    }

    public Maybe<TResult> OfType<TResult>()
    {
        if (this.HasItem)
        {
            return OfType<TResult>(this.Item);
        }

        return Empty<TResult>();
    }

    public void Exec(Action<T> action)
    {
        if (this.HasItem)
        {
            action?.Invoke(this.Item);
        }
    }

    public T GetValueOrFallback(T fallbackValue)
    {
        if (fallbackValue == null)
        {
            throw new ArgumentNullException(nameof(fallbackValue));
        }

        if (this.HasItem)
        {
            return this.Item;
        }
        else
        {
            return fallbackValue;
        }
    }

    public bool EqualsTo(T item)
    {
        return this.HasItem && Equals(this.Item, item);
    }

    public override bool Equals(object obj)
    {
        if (obj is Maybe<T> other)
        {
            return object.Equals(this.Item, other.Item);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.HasItem ? this.Item.GetHashCode() : 0;
    }

    public override string ToString()
    {
        return this.HasItem ? this.Item.ToString() : nameof(Empty);
    }
}

public class Maybe
{
    protected Maybe()
    {

    }

    public static Maybe<T> FromItem<T>(T item)
    {
        return new Maybe<T>(item);
    }

    public static Maybe<T> Empty<T>()
    {
        return Maybe<T>.Empty;
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
            return Maybe<T>.Empty;
        }

        return FromItem(item);
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
            return FromItem(t);
        }

        return Empty<T>();
    }
}