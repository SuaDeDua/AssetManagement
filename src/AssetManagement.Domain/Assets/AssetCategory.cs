using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Common;

namespace AssetManagement.Domain.Assets;

public sealed class AssetCategory : AggregateRoot<Guid>
{
    public CategoryName Name { get; private set; }
    public CategoryCode Code { get; private set; }
    public AssetType AssetType { get; private set; }

    private AssetCategory() { }
}
