namespace Category.Theory.Monads;

public abstract class Either<TLeft, TRight>
{
    public abstract bool HasLeft();

    public abstract bool HasRight();

    public abstract void IfLeft(Action<TLeft> action);

    public abstract void IfRight(Action<TRight> action);

    public abstract Either<TLeft, T1Right> Select<T1Right>(Func<TRight, T1Right> selector);

    public abstract Either<TLeft, T1Right> SelectMany<T1Right>(Func<TRight, Either<TLeft, T1Right>> selector);

    public abstract TResult Match<TResult>(Func<TLeft, TResult> Left, Func<TRight, TResult> Right);
}

public class Left<TLeft, TRight> : Either<TLeft, TRight>
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