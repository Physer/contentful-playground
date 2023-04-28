namespace ContentfulManagement.ContentTypes;

public class ContentPage : BaseType
{
    public string? Description { get; set; }
    public override string? Title { get; set; }
    public string? Content { get; set; }
}
