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
}