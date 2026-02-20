using AssetManagement.Domain.Shared.Exceptions;

namespace AssetManagement.Domain.Locations.Exceptions;

public sealed class LocationException : DomainException
{
    public LocationException()
        : base() { }
}

public sealed class EmptyAddressException : DomainException
{
    public EmptyAddressException()
        : base("Address cannot be empty") { }
}

public sealed class InvalidAddressException : DomainException
{
    public int MaxLength { get; }

    public InvalidAddressException(int maxLength)
        : base($"Address must not exceed {maxLength} characters.")
    {
        MaxLength = maxLength;
    }

    public InvalidAddressException()
        : base() { }
}
