using Category.Theory.Monads;
using Category.Theory.Nullable;

namespace Category.Theory.Tests;

public class NullableUnitTests
{
    [Fact]
    public void Nullable_CanMap()
    {
        Maybe<int> m = new Nullable<int>(4).Select(e => e + 6).Select(e => e / 2);
        Assert.True(m.HasValue());
        Assert.Equal(5, m.GetValueOrFallback(-1));
    }

    [Fact]
    public void Nullable_QueryExp_Some()
    {
        var name = new System.Nullable<char>('n');
        var id = new System.Nullable<Guid>(Guid.NewGuid());
        var addressNumber = new System.Nullable<int>(134);
        var zipNumber = new System.Nullable<int>(98101);

        var person =
            from n in name
            from i in id
            from an in addressNumber
            from z in zipNumber
            select new { n, i, an, z };

        Assert.True(person.HasValue());
        Assert.Equal(name.ToMaybe(), person.Select(e => e.n));
        Assert.Equal(id.ToMaybe(), person.Select(e => e.i));
        Assert.Equal(addressNumber.ToMaybe(), person.Select(e => e.an));
        Assert.Equal(zipNumber.ToMaybe(), person.Select(e => e.z));
    }

    [Fact]
    public void Nullable_QueryExp_None()
    {
        var name = new System.Nullable<char>('n');
        var id = (Guid?)null;
        var addressNumber = new System.Nullable<int>(134);
        var zipNumber = new System.Nullable<int>(98101);

        var person =
            from n in name
            from i in id
            from an in addressNumber
            from z in zipNumber
            select new { n, i, an, z };
        Assert.False(person.HasValue());
    }

    [Fact]
    public void Nullable_QueryExp_Null()
    {
        int? number = null;
        Guid? id = Guid.NewGuid();

        var pair =
            from n in number
            from i in id
            select new { n, i };
        Assert.False(pair.HasValue());
    }

    [Fact]
    public void Mixed_QueryExp_Some()
    {
        var name = new System.Nullable<char>('n');
        var id = Maybe.Some(Guid.NewGuid());
        var addressNumber = new System.Nullable<int>(134);
        var zipNumber = new System.Nullable<int>(98101);

        var person =
            from n in name
            from i in id
            from an in addressNumber
            from z in zipNumber
            select new { n, i, an, z };
        Assert.True(person.HasValue());
    }

    [Fact]
    public void Mixed_QueryExp_None()
    {
        var name = new System.Nullable<char>('n');
        var id = Maybe.None<Guid>();
        var addressNumber = new System.Nullable<int>(134);
        var zipNumber = new System.Nullable<int>(98101);

        var person =
            from n in name
            from i in id
            from an in addressNumber
            from z in zipNumber
            select new { n, i, an, z };
        Assert.False(person.HasValue());
    }

    [Fact]
    public void Mixed_QueryExp_Null()
    {
        int? number = null;
        var id = Maybe.Some(Guid.NewGuid());

        var pair =
            from n in number
            from i in id
            select new { n, i };
        Assert.False(pair.HasValue());
    }

    [Fact]
    public void Nullable_IfSome()
    {
        int? number = 2;

        int result = 0;
        number.IfSome(e => result = e);

        Assert.Equal(2, result);
    }

    [Fact]
    public void Nullable_EqualsTo()
    {
        int? number = 2;

        Assert.True(number.EqualsTo(2));
    }
}