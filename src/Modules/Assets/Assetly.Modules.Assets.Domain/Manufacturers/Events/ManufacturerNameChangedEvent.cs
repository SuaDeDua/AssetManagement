using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Manufacturers.Events;

public record ManufacturerNameChangedEvent(Guid ManufacturerId, string OldName, string NewName)
    : DomainEvent;
