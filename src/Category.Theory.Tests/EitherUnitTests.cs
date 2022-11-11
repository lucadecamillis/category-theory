using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class EitherUnitTests
{
    [Fact]
    public void Either_CanCreateRight()
    {
        Either<string, int> e = 3;

        Assert.True(e.HasRight());
        Assert.False(e.HasLeft());
    }

    [Fact]
    public void Either_CanCreateLeft()
    {
        Either<string, int> e = "value";

        Assert.True(e.HasLeft());
        Assert.False(e.HasRight());
    }

    [Fact]
    public void Either_CanSelect()
    {
        var e = ((Either<Exception, int>)2).Select(e => e.ToString());

        Assert.True(e.HasRight());
        Assert.Equal("2", e.GetRightOrFallback(string.Empty));
    }

    [Fact]
    public void Either_CanMap()
    {
        var e = ((Either<string, int>)(3)).Select(e => e + 5).Select(e => e / 2);
        Assert.True(e.HasRight());
        Assert.Equal(4, e.GetRightOrFallback(-1));
    }

    [Fact]
    public void Either_CanMatch()
    {
        var toString = (Either<uint, byte> e) => e.Match(u => u.ToString(), e => e.ToString());

        Either<uint, byte> b = (uint)3;
        Assert.Equal("3", toString(b));

        Either<uint, byte> u = 4;
        Assert.Equal("4", toString(u));
    }

    [Fact]
    public void Either_ImplicitOperator()
    {
        var parseSuccess = TryParseNumber("5");
        Assert.True(parseSuccess.HasRight());

        var parseFailure = TryParseNumber("5.6");
        Assert.True(parseFailure.HasLeft());
    }

    private Either<string, int> TryParseNumber(string candidate)
    {
        if (int.TryParse(candidate, out int result))
        {
            return result;
        }

        return $"Cannot parse string {candidate}";
    }
}