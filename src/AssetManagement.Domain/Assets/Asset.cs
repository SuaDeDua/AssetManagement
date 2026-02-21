using AssetManagement.Domain.Assets.Events;
using AssetManagement.Domain.Assets.Services;
using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class Asset : AggregateRoot<Guid>
{
    public Guid CompanyId { get; }
    public AssetTag Tag { get; private set; }
    public AssetName Name { get; private set; }
    public SerialNumber SerialNumber { get; private set; }
    public Guid AssetModelId { get; }
    public AssetStatus Status { get; private set; } = AssetStatus.Available;
    public Description Note { get; private set; }
    public Guid LocationId { get; }
    public bool Requestable { get; private set; }
    public string? ImageUrl { get; private set; }
    public Guid OrderInformationId { get; }

    private Asset() { }

    private Asset(
        Guid id,
        Guid companyId,
        AssetTag tag,
        AssetName name,
        SerialNumber serialNumber,
        Guid assetModelId,
        Description note,
        Guid locationId,
        Guid orderInformationId
    )
    {
        Id = id;
        CompanyId = companyId;
        Tag = tag;
        Name = name;
        SerialNumber = serialNumber;
        Status = AssetStatus.Available;
        Note = note;
        AssetModelId = assetModelId;
        LocationId = locationId;
        OrderInformationId = orderInformationId;
    }

    public static async Task<Asset> CreateAsync(
        IAssetTagProvider tagProvider,
        AssetCategory category,
        string city,
        SerialNumber serial,
        Guid companyId,
        Guid locationId,
        Guid assetModelId,
        Description note,
        Guid orderInformationId
    )
    {
        var sequence = await tagProvider.GetNextSequenceAsync(category.Code);

        var tag = AssetTag.FromSequence(category.Code, sequence);
        var name = AssetName.Create(category.Name.Value, city);

        var asset = new Asset(
            Guid.CreateVersion7(),
            companyId,
            tag,
            name,
            serial,
            assetModelId,
            note,
            locationId,
            orderInformationId
        );

        asset.AddDomainEvent(
            new AssetCreatedEvent(
                asset.Id,
                companyId,
                locationId,
                assetModelId,
                serial,
                DateTimeOffset.UtcNow
            )
        );

        return asset;
    }
}
