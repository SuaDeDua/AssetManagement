namespace Assetly.Modules.Assets.Domain.AssetModels;

public interface IAssetModelRepository
{
    Task<AssetModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByModelNoAsync(
        string modelNo,
        Guid mamufacturerId,
        CancellationToken cancellationToken = default
    );
    void Insert(AssetModel assetModel);
}
