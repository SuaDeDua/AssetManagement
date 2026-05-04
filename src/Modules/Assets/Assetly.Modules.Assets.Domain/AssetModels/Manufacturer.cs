using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Domain.AssetModels;

public sealed class Manufacturer : Entity<Guid>
{
    public string Name { get; private set; }
    public string UrlHomePage { get; private set; }
}
