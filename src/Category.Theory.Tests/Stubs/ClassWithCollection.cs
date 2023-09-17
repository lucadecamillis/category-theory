namespace Category.Theory.Stubs;

public class ClassWithCollection<T>
{
    public IEnumerable<T>? Collection { get; set; }

    public ClassWithCollection()
    {
        this.Collection = Enumerable.Empty<T>();
    }
}