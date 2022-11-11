namespace Category.Theory.Types;

public sealed class Success
{
    public static Success Instance { get; } = new Success();

    private Success()
    {
        
    }
}