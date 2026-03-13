using AssetManagement.Domain.Locations.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Locations;

public sealed class Location : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public Guid? ParentId { get; private set; }
    public string? Address { get; private set; }
    public string? Code { get; private set; }

    private Location() { }

    private Location(Guid id, Name name, Guid? parentId, string? address, string? code)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
        Address = address;
        Code = code;
    }

    public static Task<Result<Location>> Create(
        Name name,
        Guid? parentId = null,
        string? address = null,
        string? code = null
    )
    {
        var location = new Location(Guid.CreateVersion7(), name, parentId, address, code);

        location.AddDomainEvent(
            new LocationCreatedEvent(location.Id, name, parentId, address, code)
        );

        return Result<Location>.Success(location);
    }
}
