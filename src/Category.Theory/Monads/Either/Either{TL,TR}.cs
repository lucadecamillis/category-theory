namespace Category.Theory.Monads;

public abstract class Either<TLeft, TRight>
{
    public abstract bool HasLeft();

    public abstract bool HasRight();

    public abstract void IfLeft(Action<TLeft> action);

    public abstract void IfRight(Action<TRight> action);

    public abstract TRight GetRightOrFallback(TRight fallbackValue);

    public abstract Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector);

    public abstract Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector);

    public abstract TResult Match<TResult>(Func<TLeft, TResult> Left, Func<TRight, TResult> Right);
}