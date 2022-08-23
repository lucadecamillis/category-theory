namespace Category.Theory.Monads;

internal class Left<TLeft, TRight> : Either<TLeft, TRight>
{
    private readonly TLeft value;

    public Left(TLeft left)
    {
        this.value = left;
    }

    public override bool HasLeft()
    {
        return true;
    }

    public override bool HasRight()
    {
        return false;
    }

    public override void IfLeft(Action<TLeft> action)
    {
        action(value);
    }

    public override void IfRight(Action<TRight> action) { }

    public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return left(value);
    }

    public override Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector)
    {
        return new Left<TLeft, T1Right>(value);
    }

    public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
    {
        return new Left<TLeft, T1Right>(value);
    }
}