using Contentful.Core.Models;
using ContentfulManagement.ContentTypes;

namespace ContentfulManagement.Factories;

public class ContentPageFactory : IContentTypeFactory<BaseType>
{
    public ContentType CreateContentType()
    {
        return new ContentType
        {
            SystemProperties = new()
            {
                Id = Guid.NewGuid().ToString()
            },
            Description = "A content page",
            Name = "Content page",
            Fields = new()
            {
                new()
                {
                    Name = nameof(ContentPage.Title),
                    Id = nameof(ContentPage.Title).ToLower(),
                    Type = "Symbol"
                },
                new()
                {
                    Name = nameof(ContentPage.Content),
                    Id = nameof(ContentPage.Content).ToLower(),
                    Type = "RichText"
                },
                new()
                {
                    Name = nameof(ContentPage.Description),
                    Id = nameof(ContentPage.Description).ToLower(),
                    Type = "Text"
                }
            }
        };

    }
}
