using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Manufacturers.Events;

public record ManufacturerSupportPageChangedEvent(
    Guid ManufacturerId,
    string OldPage,
    string NewPage
) : DomainEvent;
