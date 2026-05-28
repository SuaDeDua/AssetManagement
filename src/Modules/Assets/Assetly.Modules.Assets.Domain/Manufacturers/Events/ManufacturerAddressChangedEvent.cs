using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Manufacturers.Events;

public record ManufacturerAddressChangedEvent(
    Guid ManufacturerId,
    string OldAddress,
    string NewAddress
) : DomainEvent;
