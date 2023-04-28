using Contentful.Core.Models;
using ContentfulManagement.ContentTypes;

namespace ContentfulManagement.Factories;

public interface IContentFactory<T> where T : BaseType
{
    string ContentTypeId { get; }
    IEnumerable<T> CreateContentItems(int count);
}
