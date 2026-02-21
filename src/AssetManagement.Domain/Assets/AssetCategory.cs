using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class AssetCategory : AggregateRoot<Guid>
{
    public CategoryName Name { get; private set; }
    public CategoryCode Code { get; private set; }
    public AssetType AssetType { get; private set; }
    public string CategoryEULA { get; private set; }
    public bool DefaultEULA { get; private set; }
    public bool NeedConfirm { get; private set; }
    public bool SendEmail { get; private set; }
    public ImageUrl Image { get; private set; }
}
