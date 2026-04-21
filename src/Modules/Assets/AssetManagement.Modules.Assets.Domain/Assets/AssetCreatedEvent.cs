using AssetManagement.Shared.Domain.Events;

namespace AssetManagement.Modules.Assets.Domain.Assets;

public record AssetCreatedEvent(
    Guid AssetId,
    string Name,
    string Description,
    string SerialNumber,
    DateTimeOffset CreatedAt
) : DomainEvent { }
