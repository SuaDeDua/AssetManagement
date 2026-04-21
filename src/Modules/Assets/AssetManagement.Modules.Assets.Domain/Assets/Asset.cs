using AssetManagement.Shared.Domain.Common;
using AssetManagement.Shared.Domain.Validation;
using AssetManagement.Shared.Domain.ValueObjects;

namespace AssetManagement.Modules.Assets.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public string SerialNumber { get; private set; } = null!;

    public AssetStatus Status { get; private set; }

    private Asset() { }

    private Asset(string name, string description, string serialNumber)
    {
        Guard.AgainstNullOrEmpty(serialNumber);

        Id = Guid.CreateVersion7();
        Name = name;
        Description = description;
        SerialNumber = serialNumber;
        Status = AssetStatus.Available;
    }

    public static Asset CreateNew(string name, string description, string serialNumber)
    {
        var asset = new Asset(name, description, serialNumber);

        asset.AddDomainEvent(
            new AssetCreatedEvent(asset.Id, name, description, serialNumber, DateTimeOffset.UtcNow)
        );

        return asset;
    }
}
