namespace Category.Theory.SumTypes;

/// <summary>
/// Sum type of 3 elements
/// Inspired by <see href="https://github.com/mcintyre321/OneOf/blob/master/OneOf/OneOfT2.generated.cs"/>
/// </summary>
/// <typeparam name="T0"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public class OneOf<T0, T1, T2>
{
    readonly T0 value0;
    readonly T1 value1;
    readonly T2 value2;
    readonly int index;

    public OneOf(T0 value0) : this(index: 0, value0: value0) { }

    public OneOf(T1 value1) : this(index: 1, value1: value1) { }

    public OneOf(T2 value2) : this(index: 2, value2: value2) { }

    private OneOf(int index, T0 value0 = default, T1 value1 = default, T2 value2 = default)
    {
        switch (index)
        {
            case 0:
                {
                    if (value0 == null)
                    {
                        throw new ArgumentNullException(nameof(value0));
                    }

                    this.value0 = value0;
                }
                break;
            case 1:
                {
                    if (value1 == null)
                    {
                        throw new ArgumentNullException(nameof(value1));
                    }

                    this.value1 = value1;
                }
                break;
            case 2:
                {
                    if (value2 == null)
                    {
                        throw new ArgumentNullException(nameof(value2));
                    }

                    this.value2 = value2;
                }
                break;
            default:
                {
                    throw new NotSupportedException($"Index {index}");
                }
        }

        this.index = index;
    }

    public static implicit operator OneOf<T0, T1, T2>(T0 t) => new OneOf<T0, T1, T2>(t);

    public static implicit operator OneOf<T0, T1, T2>(T1 t) => new OneOf<T0, T1, T2>(t);

    public static implicit operator OneOf<T0, T1, T2>(T2 t) => new OneOf<T0, T1, T2>(t);

    public TResult Match<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2)
    {
        switch (this.index)
        {
            case 0:
                {
                    return f0(this.value0);
                }
            case 1:
                {
                    return f1(this.value1);
                }
            case 2:
                {
                    return f2(this.value2);
                }
            default:
                {
                    throw new InvalidOperationException($"Malformed OneOf (Index {this.index})");
                }
        }
    }

    public bool HasT0()
    {
        return this.index == 0;
    }

    public OneOf<T0, T1, T2> IfT0(Action<T0> action)
    {
        if (this.index == 0)
        {
            action(this.value0);
        }

        return this;
    }

    public bool TryGetT0(out T0 value)
    {
        value = default(T0);

        if (this.index == 0)
        {
            value = this.value0;
            return true;
        }

        return false;
    }

    public T0 GetT0OrFallback(T0 fallbackValue)
    {
        if (this.index == 0)
        {
            return this.value0;
        }

        return fallbackValue;
    }

    public bool HasT1()
    {
        return this.index == 1;
    }

    public OneOf<T0, T1, T2> IfT1(Action<T1> action)
    {
        if (this.index == 1)
        {
            action(this.value1);
        }

        return this;
    }

    public bool TryGetT1(out T1 value)
    {
        value = default(T1);

        if (this.index == 1)
        {
            value = this.value1;
            return true;
        }

        return false;
    }

    public T1 GetT1OrFallback(T1 fallbackValue)
    {
        if (this.index == 1)
        {
            return this.value1;
        }

        return fallbackValue;
    }

    public bool HasT2()
    {
        return this.index == 2;
    }

    public OneOf<T0, T1, T2> IfT2(Action<T2> action)
    {
        if (this.index == 2)
        {
            action(this.value2);
        }

        return this;
    }

    public bool TryGetT2(out T2 value)
    {
        value = default(T2);

        if (this.index == 2)
        {
            value = this.value2;
            return true;
        }

        return false;
    }

    public T2 GetT2OrFallback(T2 fallbackValue)
    {
        if (this.index == 2)
        {
            return this.value2;
        }

        return fallbackValue;
    }
}