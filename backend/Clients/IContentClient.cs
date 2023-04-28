namespace ContentfulManagement.Clients;

public interface IContentClient
{
    Task ClearAllContentModelsAsync(CancellationToken cancellationToken = default);
    Task CreateAndPublishContentModelsAsync(CancellationToken cancellationToken = default);
    Task GenerateContentItemsAsync(CancellationToken cancellationToken = default);
}
