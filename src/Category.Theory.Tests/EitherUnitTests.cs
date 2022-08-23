using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class EitherUnitTests
{
    [Fact]
    public void Either_CanCreateRight()
    {
        Either<string, int> e = Either.Right<string, int>(3);
        Assert.True(e.HasRight());
        Assert.False(e.HasLeft());
    }

    [Fact]
    public void Either_CanCreateLeft()
    {
        Either<string, int> e = Either.Left<string, int>("value");
        Assert.True(e.HasLeft());
        Assert.False(e.HasRight());
    }

    [Fact]
    public void Either_CanSelect()
    {
        Either<Exception, string> e = Either.Right<Exception, int>(2).Select(e => e.ToString());
        Assert.True(e.HasRight());
        Assert.Equal("2", e.GetRightOrFallback(string.Empty));
    }

    [Fact]
    public void Either_CanMap()
    {
        Either<string, int> e = Either.Right<string, int>(3).Select(e => e + 5).Select(e => e / 2);
        Assert.True(e.HasRight());
        Assert.Equal(4, e.GetRightOrFallback(-1));
    }
}