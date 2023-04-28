using AutoBogus;
using Contentful.Core.Models;
using ContentfulManagement.ContentTypes;

namespace ContentfulManagement.Factories;

public class ContentPageFactory : IContentTypeFactory<BaseType>, IContentFactory<BaseType>
{
    public string ContentTypeId => "content-page";

    public IEnumerable<BaseType> CreateContentItems(int count)
    {
        var generatedContentPages = AutoFaker.Generate<ContentPage>(count);
        foreach (var contentPage in generatedContentPages)
        {
            yield return new ContentPage
            {
                Content = contentPage.Content,
                Description = contentPage.Description,
                Title = contentPage.Title,
            };
        }
    }

    public ContentType CreateContentType()
    {
        return new ContentType
        {
            SystemProperties = new()
            {
                Id = "content-page"
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
                    Type = "Text"
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
