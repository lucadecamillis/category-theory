namespace Category.Theory.Monads;

internal class Right<TLeft, TRight> : Either<TLeft, TRight>
{
    private readonly TRight value;

    public Right(TRight value)
    {
        this.value = value;
    }

    public override Either<TLeft, TRight> IfLeft(Action<TLeft> action)
    {
        return this;
    }

    public override Either<TLeft, TRight> IfRight(Action<TRight> action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        action(this.value);

        return this;
    }

    public override Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector)
    {
        return new Right<TLeft, T1Right>(selector(this.value));
    }

    public override Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector)
    {
        return selector(this.value);
    }

    public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return right(this.value);
    }

    public override bool HasLeft()
    {
        return false;
    }

    public override bool HasRight()
    {
        return true;
    }

    public override TRight GetRightOrFallback(TRight fallbackValue)
    {
        return this.value;
    }

    public override bool TryGetLeft(out TLeft value)
    {
        value = default(TLeft);
        return false;
    }

    public override bool TryGetRight(out TRight value)
    {
        value = this.value;
        return true;
    }
}