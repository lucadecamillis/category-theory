using Category.Theory.SumTypes;

namespace Category.Theory.Tests;

public class OneOfUnitTests
{
    [Fact]
    public void OneOf_CanT0()
    {
        int i = 2;

        OneOf<int, DateTime, string> o = i;

        Assert.True(o.HasT0());
        Assert.False(o.HasT1());
        Assert.False(o.HasT2());

        Assert.True(o.TryGetT0().HasValue());
        Assert.False(o.TryGetT1().HasValue());
        Assert.False(o.TryGetT2().HasValue());

        Assert.Equal(i, o.GetT0OrFallback(0));
        Assert.Equal(DateTime.MinValue, o.GetT1OrFallback(DateTime.MinValue));
        Assert.Equal(string.Empty, o.GetT2OrFallback(string.Empty));

        var hit0 = new HitTest<int>();
        o.IfT0(hit0.Action);
        Assert.True(hit0.IsHit());

        var hit1 = new HitTest<DateTime>();
        o.IfT1(hit1.Action);
        Assert.False(hit1.IsHit());

        var hit2 = new HitTest<string>();
        o.IfT2(hit2.Action);
        Assert.False(hit2.IsHit());
    }

    [Fact]
    public void OneOf_CanT1()
    {
        DateTime t = new DateTime(2018, 04, 11);

        OneOf<int, DateTime, string> o = t;

        Assert.False(o.HasT0());
        Assert.True(o.HasT1());
        Assert.False(o.HasT2());

        Assert.False(o.TryGetT0().HasValue());
        Assert.True(o.TryGetT1().HasValue());
        Assert.False(o.TryGetT2().HasValue());

        Assert.Equal(-1, o.GetT0OrFallback(-1));
        Assert.Equal(t, o.GetT1OrFallback(DateTime.MinValue));
        Assert.Equal(string.Empty, o.GetT2OrFallback(string.Empty));

        var hit0 = new HitTest<int>();
        o.IfT0(hit0.Action);
        Assert.False(hit0.IsHit());

        var hit1 = new HitTest<DateTime>();
        o.IfT1(hit1.Action);
        Assert.True(hit1.IsHit());

        var hit2 = new HitTest<string>();
        o.IfT2(hit2.Action);
        Assert.False(hit2.IsHit());
    }

    [Fact]
    public void OneOf_CanT2()
    {
        string s = "unitTest";

        OneOf<int, DateTime, string> o = s;

        Assert.False(o.HasT0());
        Assert.False(o.HasT1());
        Assert.True(o.HasT2());

        Assert.False(o.TryGetT0().HasValue());
        Assert.False(o.TryGetT1().HasValue());
        Assert.True(o.TryGetT2().HasValue());

        Assert.Equal(-1, o.GetT0OrFallback(-1));
        Assert.Equal(DateTime.MinValue, o.GetT1OrFallback(DateTime.MinValue));
        Assert.Equal(s, o.GetT2OrFallback(string.Empty));

        var hit0 = new HitTest<int>();
        o.IfT0(hit0.Action);
        Assert.False(hit0.IsHit());

        var hit1 = new HitTest<DateTime>();
        o.IfT1(hit1.Action);
        Assert.False(hit1.IsHit());

        var hit2 = new HitTest<string>();
        o.IfT2(hit2.Action);
        Assert.True(hit2.IsHit());
    }

    private class HitTest<T>
    {
        private bool isHit;

        public void Action(T item)
        {
            this.isHit = true;
        }

        public bool IsHit()
        {
            return this.isHit;
        }
    }
}