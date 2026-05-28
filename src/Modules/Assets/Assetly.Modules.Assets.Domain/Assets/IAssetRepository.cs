namespace Assetly.Modules.Assets.Domain.Assets;

public interface IAssetRepository
{
    Task<Asset?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid assetModelId, CancellationToken cancellationToken = default);
    void Insert(Asset asset);
}
