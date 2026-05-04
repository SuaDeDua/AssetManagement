using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Domain.AssetModels;

public sealed class AssetModel : AggregateRoot<Guid>
{
    public Guid ManufacturerId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid FieldSetId { get; private set; }
    public string Name { get; private set; }
    public string ModelNo { get; private set; }
    public Description? Description { get; private set; }
}
