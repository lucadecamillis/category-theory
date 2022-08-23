namespace Category.Theory.Monads;

public abstract class Maybe
{
    protected Maybe()
    {
        
    }

    public static Maybe<T> Some<T>(T item)
    {
        return new Some<T>(item);
    }

    public static Maybe<T> None<T>()
    {
        return Category.Theory.Monads.None<T>.Instance;
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
            return None<T>();
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