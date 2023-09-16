using Category.Theory.Monads;
using Category.Theory.Types;

namespace Category.Theory.Tests;

public class MaybeUnitTests
{
    [Fact]
    public void Maybe_CanCreateEmpty()
    {
        Maybe<int> m = None.Instance;
        Assert.False(m.HasValue());
        Assert.False(m.TryGetValue(out _));
    }

    [Fact]
    public void Maybe_CanCreateSome()
    {
        Maybe<int> m = 4;
        Assert.True(m.HasValue());
        Assert.True(m.TryGetValue(out var value) && value == 4);
        Assert.Equal(4, m.GetValueOrFallback(-1));
    }

    [Fact]
    public void Maybe_CanMap()
    {
        Maybe<int> m = Maybe.Some(4).Select(e => e + 6).Select(e => e / 2);
        Assert.True(m.HasValue());
        Assert.Equal(5, m.GetValueOrFallback(-1));
    }

    [Fact]
    public void Maybe_CanQuery()
    {
        Maybe<int> m = 4;

        var result =
            from value in m
            where value == 2
            select value;

        Assert.False(result.HasValue());
    }

    [Fact]
    public void Maybe_QueryExp_Some()
    {
        Maybe<string> name = "name";
        Maybe<string> address = "address";
        Maybe<int> addressNumber = 134;
        Maybe<int> zipNumber = 98101;

        var person =
            from n in name
            from a in address
            from an in addressNumber
            from z in zipNumber
            select new { n, a, an, z };

        Assert.True(person.HasValue());
        Assert.Equal(name, person.Select(e => e.n));
        Assert.Equal(address, person.Select(e => e.a));
        Assert.Equal(addressNumber, person.Select(e => e.an));
        Assert.Equal(zipNumber, person.Select(e => e.z));
    }

    [Fact]
    public void Maybe_QueryExp_None()
    {
        Maybe<string> name = "name";
        Maybe<string> address = "address";
        Maybe<int> addressNumber = None.Instance;
        Maybe<int> zipNumber = 98101;

        var person =
            from n in name
            from a in address
            from an in addressNumber
            from z in zipNumber
            select new { n, a, an, z };
        Assert.False(person.HasValue());
    }

    [Fact]
    public void Maybe_SelectNullCollection()
    {
        var c = new Stubs.ClassWithCollection<int>();
        var m = Maybe.Some(c).AsEnumerable(e => e.Collection);
        Assert.Empty(m);
    }

    [Fact]
    public void Maybe_SelectEmptyCollection()
    {
        var c = new Stubs.ClassWithCollection<int> { Collection = Array.Empty<int>() };
        var m = Maybe.Some(c).AsEnumerable(e => e.Collection);
        Assert.Empty(m);
    }
}