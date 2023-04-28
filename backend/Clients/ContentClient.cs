using Contentful.Core;
using Contentful.Core.Configuration;
using ContentfulManagement.ContentTypes;
using ContentfulManagement.Factories;
using Microsoft.Extensions.Options;

namespace ContentfulManagement.Clients;

public class ContentClient : IContentClient
{
    private readonly ContentfulOptions _contentfulOptions;
    private readonly IContentfulClient _contentfulDeliveryClient;
    private readonly IContentfulManagementClient _contentfulManagementClient;
    private readonly IEnumerable<IContentTypeFactory<BaseType>> _contentTypeFactories;
    private readonly IEnumerable<IContentFactory<BaseType>> _contentFactories;

    public ContentClient(HttpClient httpClient,
        IOptions<ContentfulOptions> contentfulOptions,
        IEnumerable<IContentTypeFactory<BaseType>> contentTypeFactories,
        IEnumerable<IContentFactory<BaseType>> contentFactories)
    {
        _contentfulOptions = contentfulOptions.Value;
        _contentfulDeliveryClient = new ContentfulClient(httpClient, contentfulOptions.Value);
        _contentfulManagementClient = new ContentfulManagementClient(httpClient, contentfulOptions.Value);
        _contentTypeFactories = contentTypeFactories;
        _contentFactories = contentFactories;
    }

    public void ClearAllContent() => throw new NotImplementedException();

    public async Task ClearAllContentModelsAsync(CancellationToken cancellationToken = default)
    {
        var contentTypes = await _contentfulManagementClient.GetContentTypes();
        foreach (var contentType in contentTypes)
        {
            if (contentType.SystemProperties?.FirstPublishedAt is not null)
                await _contentfulManagementClient.DeactivateContentType(contentType.SystemProperties?.Id, _contentfulOptions?.SpaceId, cancellationToken);

            await _contentfulManagementClient.DeleteContentType(contentType.SystemProperties?.Id, _contentfulOptions?.SpaceId, cancellationToken);
        }
    }

    public async Task CreateAndPublishContentModelsAsync(CancellationToken cancellationToken = default)
    {
        foreach (var factory in _contentTypeFactories)
        {
            var contentType = factory.CreateContentType();
            var createdContentType = await _contentfulManagementClient.CreateOrUpdateContentType(contentType, _contentfulOptions?.SpaceId, cancellationToken: cancellationToken);
            await _contentfulManagementClient.ActivateContentType(createdContentType.SystemProperties.Id, createdContentType.SystemProperties.Version ?? 0, _contentfulOptions?.SpaceId, cancellationToken);
        }
    }

    public async Task GenerateContentItemsAsync(CancellationToken cancellationToken = default)
    {
        foreach (var factory in _contentFactories)
        {
            var contentTypeId = await _contentfulManagementClient.GetContentType(factory.ContentTypeId, _contentfulOptions?.SpaceId, cancellationToken);
            var items = factory.CreateContentItems(25);
            foreach(var item in items)
            {
                await _contentfulManagementClient.CreateEntryForLocale(item, Guid.NewGuid().ToString(), factory.ContentTypeId, null, null, cancellationToken);
            }
        }
    }
}
