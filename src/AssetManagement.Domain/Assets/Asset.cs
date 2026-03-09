using AssetManagement.Domain.Assets.Events;
using AssetManagement.Domain.Assets.Services;
using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Locations;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public Guid CompanyId { get; }
    public Guid AssetModelId { get; }
    public Guid LocationId { get; }
    public Guid AssetCategoryId { get; }
    public Guid OrderInformationId { get; }
    public Guid? AssignedUserId { get; }
    public AssetTag Tag { get; private set; }
    public AssetName Name { get; private set; }
    public AssetStatus Status { get; private set; }
    public SerialNumber SerialNumber { get; private set; }
    public HardwareProfile Profile { get; private set; }
    public Description Note { get; private set; }
    public bool Requestable { get; private set; }
    public ImageUrl Image { get; private set; }

    private Asset() { }

    private Asset(
        Guid id,
        Guid companyId,
        AssetTag tag,
        AssetName name,
        AssetStatus status,
        HardwareProfile profile,
        SerialNumber serialNumber,
        Guid assetModelId,
        Description note,
        Guid locationId,
        Guid categoryId,
        Guid orderInformationId
    )
    {
        Id = id;
        CompanyId = companyId;
        Tag = tag;
        Name = name;
        Status = status;
        Profile = profile;
        SerialNumber = serialNumber;
        Note = note;
        AssetModelId = assetModelId;
        LocationId = locationId;
        AssetCategoryId = categoryId;
        OrderInformationId = orderInformationId;
    }

    public static async Task<Result<Asset>> CreateAssetAsync(
        IAssetTagProvider tagProvider,
        AssetCategory category,
        SerialNumber serial,
        Guid companyId,
        Location location,
        Guid assetModelId,
        Description note,
        Guid orderInformationId,
        HardwareProfile? profile = null
    )
    {
        var sequence = await tagProvider.GetNextSequenceAsync(category.Code);
        var tag = AssetTag.FromSequence(category.Code, sequence);

        var name = AssetName.Create(category.Name.Value, location.City.Value);

        var finalStatus = AssetStatus.Available;
        var finalProfile = profile ?? HardwareProfile.Default;

        var asset = new Asset(
            Guid.CreateVersion7(),
            companyId,
            tag,
            name,
            finalStatus,
            finalProfile,
            serial,
            assetModelId,
            note,
            location.Id,
            category.Id,
            orderInformationId
        );

        asset.AddDomainEvent(
            new AssetCreatedEvent(
                asset.Id,
                companyId,
                location.Id,
                assetModelId,
                category.Id,
                serial,
                DateTimeOffset.UtcNow
            )
        );

        return Result<Asset>.Success(asset);
    }
}
