namespace Category.Theory.Monads;

public class Right<TLeft, TRight> : Either<TLeft, TRight>
{
    private readonly TRight value;

    public Right(TRight value)
    {
        this.value = value;
    }

    public override void IfLeft(Action<TLeft> action) { }

    public override void IfRight(Action<TRight> action)
    {
        action(value);
    }

    public override Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector)
    {
        return new Right<TLeft, T1Right>(selector(value));
    }

    public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
    {
        return selector(value);
    }

    public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return right(value);
    }

    public override bool HasLeft()
    {
        return false;
    }

    public override bool HasRight()
    {
        return true;
    }
}