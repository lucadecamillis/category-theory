namespace Category.Theory.Stubs;

public class SimpleException : Exception
{
    public SimpleException() { }

    public SimpleException(string? message) : base(message) { }

    public SimpleException(string? message, Exception? innerException) : base(message, innerException) { }
}