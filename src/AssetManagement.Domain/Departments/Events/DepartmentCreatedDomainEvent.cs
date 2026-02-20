using AssetManagement.Domain.Shared;
using AssetManagement.Domain.Shared.Events;

namespace AssetManagement.Domain.Departments.Events;

public sealed record DepartmentCreatedEvent(Guid DepartmentId, Guid CompanyId, Name Name, Code Code)
    : DomainEvent;
