using Assetly.Modules.Assets.Domain.Manufacturers.Events;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.Validation;

namespace Assetly.Modules.Assets.Domain.Manufacturers;

public sealed class Manufacturer : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;

    public string SupportPage { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    private Manufacturer() { }

    private Manufacturer(string name, string supportPage, string address)
    {
        Guard.AgainstNullOrEmpty(name);
        Guard.AgainstNullOrEmpty(supportPage);

        Id = Guid.CreateVersion7();
        Name = name;
        SupportPage = supportPage;
        Address = address;
    }

    public static Manufacturer Create(string name, string supportPage, string address)
    {
        var manufacturer = new Manufacturer(name, supportPage, address);

        manufacturer.AddDomainEvent(new ManufacturerCreatedEvent(manufacturer.Id));

        return manufacturer;
    }

    public Result ChangeName(string newName)
    {
        Guard.AgainstNullOrEmpty(newName);

        if (Name == newName)
            return Result.Success();

        string oldName = Name;

        Name = newName;

        AddDomainEvent(new ManufacturerNameChangedEvent(Id, oldName, newName));

        return Result.Success();
    }

    public Result ChangePage(string newPage)
    {
        Guard.AgainstNullOrEmpty(newPage);

        if (SupportPage == newPage)
            return Result.Success();

        string oldPage = SupportPage;

        SupportPage = newPage;

        AddDomainEvent(new ManufacturerSupportPageChangedEvent(Id, oldPage, newPage));

        return Result.Success();
    }

    public Result ChangeAddress(string newAddress)
    {
        Guard.AgainstNullOrEmpty(newAddress);

        if (Address == newAddress)
            return Result.Success();

        string oldAddress = Address;

        Address = newAddress;

        AddDomainEvent(new ManufacturerAddressChangedEvent(Id, oldAddress, newAddress));

        return Result.Success();
    }
}
