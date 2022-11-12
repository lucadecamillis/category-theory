namespace Category.Theory.Types;

/// <summary>
/// Represents the empty value in sum types
/// </summary>
public sealed class None
{
    public static None Instance { get; } = new None();

    private None()
    {

    }
}