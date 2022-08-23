namespace Category.Theory.Monads;

public abstract class Maybe<T>
{
    protected Maybe()
    {

    }

    public abstract bool HasValue();

    public abstract T GetValueOrThrow(string errorMessage = null);

    public abstract T GetValueOrFallback(T fallbackValue);

    public abstract Maybe<TResult> Select<TResult>(Func<T, TResult> selector);

    public abstract Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector);

    public abstract TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc);

    public abstract void Iter(Action<T> someAction, Action noneAction);

    public abstract Maybe<T> Where(Func<T, bool> predicate);

    public abstract void Exec(Action<T> action);

    public abstract bool EqualsTo(T item);

    public Maybe<TResult> OfType<TResult>()
    {
        return this.SelectMany(e => Maybe.OfType<TResult>(e));
    }
}