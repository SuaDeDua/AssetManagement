using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public string SerialNumber { get; private set; } = null!;

    public AssetStatus Status { get; private set; }

    private Asset() { }

    private Asset(string name, string description, string serialNumber)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Description = description;
        SerialNumber = serialNumber;
        Status = AssetStatus.Available;
    }

    public static Asset Create(string name, string description, string serialNumber)
    {
        var asset = new Asset(name, description, serialNumber);

        asset.AddDomainEvent(new AssetCreatedEvent(asset.Id, name, description, serialNumber));
        return asset;
    }
}
