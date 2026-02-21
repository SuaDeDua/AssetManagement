using AssetManagement.Domain.Shared.Common;

namespace AssetManagement.Domain.Assets;

public sealed class AssetModel : AggregateRoot<Guid>
{
    public Guid CategoryId { get; }
}
