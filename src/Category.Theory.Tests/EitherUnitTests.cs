using Category.Theory.Monads;

namespace Category.Theory.Tests;

public class EitherUnitTests
{
    [Fact]
    public void Either_CanSelect()
    {
        Either<Exception, int> entity = new Right<Exception, int>(2);

        var stringEntity = entity.Select(e => e.ToString());

        
    }
}