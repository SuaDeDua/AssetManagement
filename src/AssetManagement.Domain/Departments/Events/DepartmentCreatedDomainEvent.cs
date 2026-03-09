using AssetManagement.Domain.Shared.Events;

namespace AssetManagement.Domain.Departments.Events;

public sealed record DepartmentCreatedEvent(Guid DepartmentId, Guid CompanyId) : DomainEvent;
