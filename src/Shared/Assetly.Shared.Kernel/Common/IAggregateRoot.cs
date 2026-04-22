using Assetly.Shared.Kernel.Events;

namespace Assetly.Shared.Kernel.Common;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
