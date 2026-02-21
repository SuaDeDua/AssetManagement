using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Events;

namespace AssetManagement.Domain.Assets.Events;

public record AssetCreatedEvent(
    Guid AssetId,
    Guid CompanyId,
    Guid LocationId,
    Guid AssetModelId,
    SerialNumber SerialNumber,
    DateTimeOffset CreatedAt
) : DomainEvent { }
