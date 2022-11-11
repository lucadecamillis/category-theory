namespace Category.Theory.SumTypes;

/// <summary>
/// Inspired by <see href="https://github.com/mcintyre321/OneOf/blob/master/OneOf/OneOfT2.generated.cs"/>
/// </summary>
/// <typeparam name="T0"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public class OneOf<T0, T1, T2>
{
    readonly T0 _value0;
    readonly T1 _value1;
    readonly T2 _value2;
    readonly int _index;

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

                    _value0 = value0;
                }
                break;
            case 1:
                {
                    if (value1 == null)
                    {
                        throw new ArgumentNullException(nameof(value1));
                    }

                    _value1 = value1;
                }
                break;
            case 2:
                {
                    if (value2 == null)
                    {
                        throw new ArgumentNullException(nameof(value2));
                    }

                    _value2 = value2;
                }
                break;
            default:
                {
                    throw new NotSupportedException($"Index {index}");
                }
        }

        _index = index;
    }

    public static implicit operator OneOf<T0, T1, T2>(T0 t) => new OneOf<T0, T1, T2>(0, value0: t);

    public static implicit operator OneOf<T0, T1, T2>(T1 t) => new OneOf<T0, T1, T2>(1, value1: t);

    public static implicit operator OneOf<T0, T1, T2>(T2 t) => new OneOf<T0, T1, T2>(2, value2: t);

    public TResult Match<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1, Func<T2, TResult> f2)
    {
        switch (_index)
        {
            case 0:
                {
                    return f0(_value0);
                }
            case 1:
                {
                    return f1(_value1);
                }
            case 2:
                {
                    return f2(_value2);
                }
            default:
                {
                    throw new InvalidOperationException($"Malformed OneOf (Index {_index})");
                }
        }
    }

    public bool HasT0()
    {
        return _index == 0;
    }

    public OneOf<T0, T1, T2> IfT0(Action<T0> action)
    {
        if (_index == 0)
        {
            action(_value0);
        }

        return this;
    }

    public bool TryGetT0(out T0 value)
    {
        value = default(T0);

        if (_index == 0)
        {
            value = _value0;
            return true;
        }

        return false;
    }

    public T0 GetT0OrFallback(T0 fallbackValue)
    {
        if (_index == 0)
        {
            return _value0;
        }

        return fallbackValue;
    }

    public bool HasT1()
    {
        return _index == 1;
    }

    public OneOf<T0, T1, T2> IfT1(Action<T1> action)
    {
        if (_index == 1)
        {
            action(_value1);
        }

        return this;
    }

    public bool TryGetT1(out T1 value)
    {
        value = default(T1);

        if (_index == 1)
        {
            value = _value1;
            return true;
        }

        return false;
    }

    public T1 GetT1OrFallback(T1 fallbackValue)
    {
        if (_index == 1)
        {
            return _value1;
        }

        return fallbackValue;
    }

    public bool HasT2()
    {
        return _index == 2;
    }

    public OneOf<T0, T1, T2> IfT2(Action<T2> action)
    {
        if (_index == 2)
        {
            action(_value2);
        }

        return this;
    }

    public bool TryGetT2(out T2 value)
    {
        value = default(T2);

        if (_index == 2)
        {
            value = _value2;
            return true;
        }

        return false;
    }

    public T2 GetT2OrFallback(T2 fallbackValue)
    {
        if (_index == 2)
        {
            return _value2;
        }

        return fallbackValue;
    }
}