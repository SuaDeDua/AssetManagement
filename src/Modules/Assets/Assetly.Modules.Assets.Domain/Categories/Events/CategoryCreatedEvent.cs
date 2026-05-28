using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Categories.Events;

public record CategoryCreatedEvent(Guid CategoryId) : DomainEvent;
