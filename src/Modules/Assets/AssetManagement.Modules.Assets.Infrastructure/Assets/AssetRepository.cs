using AssetManagement.Modules.Assets.Domain.Assets;
using AssetManagement.Modules.Assets.Infrastructure.Database;

namespace AssetManagement.Modules.Assets.Infrastructure.Assets;

internal sealed class AssetRepository(AssetsDbContext context) : IAssetRepository
{
    public void Insert(Asset asset)
    {
        context.Assets.Add(asset);
    }
}
