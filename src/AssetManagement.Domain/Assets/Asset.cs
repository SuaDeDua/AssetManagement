using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class Assets : AggregateRoot<Guid>
{
    public Guid CompanyId { get; }
    public AssetTag AssetTag { get; private set; }
    public SerialNumber SerialNumber { get; private set; }
    public Guid AssetModelId { get; }
    public AssetStatus Status { get; private set; }
    public Description? Notes { get; private set; }
    public Location DefaultLocation { get; private set; }
    public bool Requestable { get; private set; }
    public string? ImageUrl { get; private set; }
    public AssetName Name { get; private set; }
    public Guid OrderInformationId { get; private set; }
}
