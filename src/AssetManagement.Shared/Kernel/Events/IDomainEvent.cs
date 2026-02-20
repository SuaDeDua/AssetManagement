namespace AssetManagement.Shared.Kernel.Events;

public interface IDomainEvent
{
    Guid EventId { get; }

    DateTimeOffset OccurredOn { get; }

    int Version { get; }
}
