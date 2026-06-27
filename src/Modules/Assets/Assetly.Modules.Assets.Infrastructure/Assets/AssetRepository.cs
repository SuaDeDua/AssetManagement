using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Modules.Assets.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Assetly.Modules.Assets.Infrastructure.Assets;

internal sealed class AssetRepository(AssetsDbContext context) : IAssetRepository
{
    public async Task<Asset?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Assets.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Insert(Asset asset)
    {
        context.Assets.Add(asset);
    }
}
