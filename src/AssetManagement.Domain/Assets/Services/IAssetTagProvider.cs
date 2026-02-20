using AssetManagement.Domain.Assets.ValueObjects;

namespace AssetManagement.Domain.Assets.Services;

public interface IAssetTagProvider
{
    Task<int> GetNextSequenceAsync(
        CategoryCode categoryCode,
        CancellationToken cancellationToken = default
    );
}
