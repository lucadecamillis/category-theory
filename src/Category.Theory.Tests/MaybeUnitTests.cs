using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class MaybeUnitTests
{
    [Fact]
    public void Maybe_CanCreateEmpty()
    {
        Maybe<int> m = Maybe.Empty<int>();
        Assert.False(m.HasItem);
    }

    [Fact]
    public void Maybe_CanCreateSome()
    {
        Maybe<int> m = Maybe.FromItem(4);
        Assert.True(m.HasItem);
        Assert.Equal(4, m.GetValueOrFallback(-1));
    }

    [Fact]
    public void Maybe_CanMap()
    {
        Maybe<int> m = Maybe.FromItem(4).Select(e => e + 6).Select(e => e / 2);
        Assert.True(m.HasItem);
        Assert.Equal(5, m.GetValueOrFallback(-1));
    }

    [Fact]
    public void Maybe_CanUseQueryExp()
    {
        var name = Maybe.FromItem("name");
        var address = Maybe.FromItem("address");
        var addressNumber = Maybe.FromItem(134);
        var zipNumber = Maybe.FromItem(98101);

        var person =
            from n in name
            from a in address
            from an in addressNumber
            from z in zipNumber
            select new { n, a, an, z };
        
        Assert.True(person.HasItem);
        Assert.Equal(name, person.Select(e => e.n));
        Assert.Equal(address, person.Select(e => e.a));
        Assert.Equal(addressNumber, person.Select(e => e.an));
        Assert.Equal(zipNumber, person.Select(e => e.z));
    }
}