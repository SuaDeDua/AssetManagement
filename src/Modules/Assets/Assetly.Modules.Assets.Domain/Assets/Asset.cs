using Assetly.Modules.Assets.Domain.AssetModels;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;
using ErrorOr;

namespace Assetly.Modules.Assets.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public string Description { get; private set; } = string.Empty;

    public string SerialNumber { get; private set; } = string.Empty;

    public AssetStatus Status { get; private set; }

    public Guid AssetModelId { get; }

    public Guid? AssignedUserId { get; private set; }

    private Asset() { }

    private Asset(string description, string serialNumber, Guid assetModelId)
    {
        Id = Guid.CreateVersion7();
        Description = description;
        SerialNumber = serialNumber;
        AssetModelId = assetModelId;
        Status = AssetStatus.Available;
        AssignedUserId = null;
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

    public ErrorOr<Success> AssignToUserId(Guid assignedUserId)
    {
        if (Status != AssetStatus.Available)
        {
            return Error.Conflict(
                code: "Asset.Unavailable",
                description: "The asset is currenly unavailable for assignment."
            );
        }
        if (AssignedUserId is not null)
        {
            return Error.Conflict(
                code: "Asset.AlreadyAssigned",
                description: "This asset has already been assiged to another user."
            );
        }

        AssignedUserId = assignedUserId;
        Status = AssetStatus.InUse;

        AddDomainEvent(new AssetAssignedEvent(Id, assignedUserId));
        return Result.Success;
    }
}
