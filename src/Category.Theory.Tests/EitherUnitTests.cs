using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class EitherUnitTests
{
    [Fact]
    public void Either_CanCreateRight()
    {
        Either<string, int> e = 3;

        string left; int right;

        Assert.True(e.HasRight());
        Assert.True(e.TryGetRight(out right, out left));
        Assert.Equal(3, right);
        Assert.Null(left);

        Assert.False(e.HasLeft());
        Assert.False(e.TryGetLeft(out left, out right));
        Assert.Equal(3, right);
        Assert.Null(left);
    }

    [Fact]
    public void Either_CanCreateLeft()
    {
        string value = nameof(value);
        Either<string, int> e = value;

        string left; int right;

        Assert.True(e.HasLeft());
        Assert.True(e.TryGetLeft(out left, out right));
        Assert.Equal(value, left);
        Assert.Equal(0, right);

        Assert.False(e.HasRight());
        Assert.False(e.TryGetRight(out right, out left));
        Assert.Equal(value, left);
        Assert.Equal(0, right);
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

    [Fact]
    public void Either_CanTraverse()
    {
        Either<string, int>[] collection = { 2, 3, 4, 5 };
        Assert.Equal(new[] { 2, 3, 4, 5 }, collection.Traverse(e => e).GetRightOrFallback(Array.Empty<int>()));
    }

    [Fact]
    public void Either_CannotTraverse()
    {
        Either<string, int>[] collection = { 2, 3, "error", 4, 5 };
        Assert.True(collection.Traverse(e => e).HasLeft());
    }

    [Fact]
    public void Either_EqualToLeft()
    {
        Assert.True(Either.Left<int, string>(4).EqualsTo(4));
        Assert.False(Either.Right<int, string>("s").EqualsTo(4));
    }

    [Fact]
    public void Either_EqualToRight()
    {
        Assert.True(Either.Right<int, string>("s").EqualsTo("s"));
        Assert.False(Either.Left<int, string>(4).EqualsTo("s"));
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