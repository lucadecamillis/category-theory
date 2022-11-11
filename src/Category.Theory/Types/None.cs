namespace Category.Theory.Types;

public sealed class None
{
    public static None Instance { get; } = new None();

    private None()
    {

    }
}