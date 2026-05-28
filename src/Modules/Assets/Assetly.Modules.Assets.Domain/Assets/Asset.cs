using Assetly.Modules.Assets.Domain.AssetModels;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;
using ErrorOr;

namespace Assetly.Modules.Assets.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public string Description { get; private set; } = null!;

    public string SerialNumber { get; private set; } = null!;

    public AssetStatus Status { get; private set; }

    public Guid AssetModelId { get; private set; }

    private Asset() { }

    private Asset(string description, string serialNumber, Guid assetModelId)
    {
        Id = Guid.CreateVersion7();
        Description = description;
        SerialNumber = serialNumber;
        Status = AssetStatus.Available;
        AssetModelId = assetModelId;
    }

    public static ErrorOr<Asset> Create(
        string description,
        string serialNumber,
        AssetModel assetModel
    )
    {
        var asset = new Asset(description, serialNumber, assetModel.Id);

        asset.AddDomainEvent(new AssetCreatedEvent(asset.Id));
        return asset;
    }
}
