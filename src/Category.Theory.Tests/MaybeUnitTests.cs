using Category.Theory.Monads;
using Category.Theory.Stubs;
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
        Assert.True(m.TryGetValue(out var value));
        Assert.Equal(4, value);
    }

    [Fact]
    public void Maybe_CanSelect()
    {
        Maybe<int> m = Maybe.Some(4).Select(e => e + 6).Select(e => e / 2);
        Assert.Equal(5, m);
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
        var c = new ClassWithCollection<int>();
        var m = Maybe.Some(c).AsEnumerable(e => e.Collection);
        Assert.Empty(m);
    }

    [Fact]
    public void Maybe_SelectEmptyCollection()
    {
        var c = new ClassWithCollection<int> { Collection = Array.Empty<int>() };
        var m = Maybe.Some(c).AsEnumerable(e => e.Collection);
        Assert.Empty(m);
    }

    [Fact]
    public void Maybe_CanCheckNull()
    {
        SimpleClass? item = null;
        Assert.False(Maybe.CheckNull(item).HasValue());

        item = new SimpleClass(3);
        Assert.True(Maybe.CheckNull(item).HasValue());
    }

    [Fact]
    public void Maybe_CanCheckOfType()
    {
        object item = new SimpleClass(4);
        Assert.True(Maybe.CheckNull(item).OfType<SimpleClass>().HasValue());
    }

    [Fact]
    public void Maybe_CanEqualsTo()
    {
        Assert.True(Maybe.Some(5).EqualsTo(5));
        Assert.False(Maybe.Some(5).EqualsTo(4));
        Assert.False(Maybe.None<int>().EqualsTo(3));

        Assert.Equal(5, Maybe.Some(5));
    }

    [Fact]
    public void Maybe_CanMatch()
    {
        Maybe<int> m = 5;
        Assert.Equal(8, m.Match(e => e + 3, () => -1));
        Assert.Equal(-1, Maybe.None<int>().Match(e => e + 1, () => -1));
    }

    [Fact]
    public void Maybe_ThrowIfNone()
    {
        var m = Maybe.None<int>();
        Assert.Throws<SimpleException>(() => m.GetValueOrThrow(new SimpleException("Exception")));
    }

    [Fact]
    public void Maybe_CanFlatMap()
    {
        var m = Maybe.Some<Maybe<int>>(5);
        Assert.True(m.FlatMap().EqualsTo(5));
    }

    [Fact]
    public void Maybe_CanTryFirst()
    {
        var c = new[] { 3, 4, 5 };

        var first = c.TryFirst();
        Assert.Equal(3, first);
    }

    [Fact]
    public void Maybe_CanTryFirstWithPredicate()
    {
        var c = new[] { 3, 4, 5 }.Select(i => new SimpleClass(i));

        var first = c.TryFirst(e => e.Value > 3);

        Assert.Equal(4, first.Select(e => e.Value));
    }

    [Fact]
    public void Maybe_CanEqualsToWithStringComparison()
    {
        var m = Maybe.Some("some string");

        Assert.True(m.EqualsTo("Some String", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Maybe_If()
    {
        var m = Maybe.Some(3);

        Assert.True(m.If(e => e > 2));
        Assert.False(m.If(e => e > 3));
    }

    [Fact]
    public void Maybe_If_None()
    {
        Maybe<int> m = None.Instance;

        Assert.False(m.If(e => e > 2));
        Assert.False(m.If(e => e > 3));
    }

    [Fact]
    public void Maybe_Equal_None()
    {
        Assert.Equal(None.Instance, None.Instance);
    }

    [Fact]
    public void Maybe_Equal_Some()
    {
        Assert.Equal(Maybe.Some(3), Maybe.Some(3));
    }

    [Fact]
    public void Maybe_NotEqual_Some()
    {
        Assert.NotEqual(Maybe.Some(3), Maybe.Some(2));
    }

    [Fact]
    public void Maybe_NotEqual_Some_None()
    {
        Assert.NotEqual(Maybe.Some(3), None.Instance);
    }

    [Fact]
    public void Maybe_CanSelectNullString()
    {
        Assert.False(((string?)null).TrySelectString().HasValue());
    }

    [Fact]
    public void Maybe_CanSelectEmptyString()
    {
        Assert.False(string.Empty.TrySelectString().HasValue());
    }

    [Fact]
    public void Maybe_CanSelectString()
    {
        Assert.Equal("sample", "sample".TrySelectString());
    }

    [Fact]
    public void Maybe_Or_BothNone()
    {
        Assert.False(Maybe.None<string>().Or(() => Maybe.None<string>()).HasValue());
    }

    [Fact]
    public void Maybe_Or_First()
    {
        Assert.Equal("or", Maybe.Some("or").Or(() => None.Instance));
    }

    [Fact]
    public void Maybe_Or_Second()
    {
        Assert.Equal("or", Maybe.None<string>().Or(() => Maybe.Some("or")));
    }

    [Fact]
    public void Maybe_Or_ChooseFirst()
    {
        Assert.Equal("or1", Maybe.Some("or1").Or(() => Maybe.Some("or2")));
    }
}