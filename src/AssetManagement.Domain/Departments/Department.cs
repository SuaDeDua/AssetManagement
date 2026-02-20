using AssetManagement.Domain.Departments.Events;
using AssetManagement.Domain.Shared;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.Validation;

namespace AssetManagement.Domain.Departments;

public sealed class Department : AggregateRoot<Guid>
{
    public Name Name { get; private set; } = null!;

    public Code Code { get; private set; } = null!;

    public Guid CompanyId { get; }
    public Guid? ManagerId { get; private set; }

    private Department() { }

    private Department(Name name, Code code, Guid companyId)
    {
        Guard.AgainstNullOrEmpty(nameof(name));
        Guard.AgainstNullOrEmpty(nameof(code));

        Id = Guid.CreateVersion7();
        Name = name;
        Code = code;
        CompanyId = companyId;
    }

    public static Result<Department> Create(Name name, Code code, Guid companyId)
    {
        var department = new Department(Guid.CreateVersion7(), name, code, companyId);

        department.AddDomainEvent(
            new DepartmentCreatedEvent(department.Id, department.Name, department.Code, companyId)
        );

        return department;
    }

    public static Result<Department> Update(
        Name name,
        Code code,
        Guid companyId,
        Description? description
    )
    {
        department.Raise(new DepartmentCreatedDomainEvent(department.Id));

        return department;
    }
}
