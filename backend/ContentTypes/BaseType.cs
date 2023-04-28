namespace ContentfulManagement.ContentTypes;

public abstract class BaseType
{
    public abstract string Id { get; }
    public abstract string? Title { get; set; }
}
