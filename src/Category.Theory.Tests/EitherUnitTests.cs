using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class EitherUnitTests
{
    [Fact]
    public void Either_CanCreateRight()
    {
        Either<string, int> e = new Right<string, int>(3);
        Assert.True(e.HasRight());
        Assert.False(e.HasLeft());
    }

    [Fact]
    public void Either_CanCreateLeft()
    {
        Either<string, int> e = new Left<string, int>("value");
        Assert.True(e.HasLeft());
        Assert.False(e.HasRight());
    }

    [Fact]
    public void Either_CanSelect()
    {
        Either<Exception, int> entity = new Right<Exception, int>(2);

        var stringEntity = entity.Select(e => e.ToString());
    }
}