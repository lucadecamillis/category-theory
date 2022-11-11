using Category.Theory.SumTypes;

namespace Category.Theory.Tests;

public class OneOfUnitTests
{
    [Fact]
    public void OneOf_CanT0()
    {
        OneOf<int, DateTime, string> o = 2;

        Assert.True(o.HasT0());
        Assert.False(o.HasT1());
        Assert.False(o.HasT2());

        Assert.True(o.TryGetT0().HasValue());
        Assert.False(o.TryGetT1().HasValue());
        Assert.False(o.TryGetT2().HasValue());
    }
}