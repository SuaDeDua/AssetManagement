using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Modules.Assets.Infrastructure.Database;

namespace Assetly.Modules.Assets.Infrastructure.Assets;

internal sealed class AssetRepository(AssetsDbContext context) : IAssetRepository
{
    public void Insert(Asset asset)
    {
        context.Assets.Add(asset);
    }
}
