using AssetManagement.Domain.Shared.Events;

namespace AssetManagement.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : DomainEvent { }
