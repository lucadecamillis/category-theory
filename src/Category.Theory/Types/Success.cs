namespace Category.Theory.Types;

/// <summary>
/// Represents the success value in sum types
/// </summary>
public sealed class Success
{
    public static Success Instance { get; } = new Success();

    private Success()
    {
        
    }
}