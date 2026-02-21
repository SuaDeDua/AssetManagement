using AssetManagement.Domain.Locations.Exceptions;

namespace AssetManagement.Domain.Locations.ValueObjects;

public record Address
{
    const int MaxLength = 200;
    public string Value { get; }

    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyAddressException();

        if (value.Length > MaxLength)
            throw new InvalidAddressException(MaxLength);

        Value = value.Trim();
    }

    public static implicit operator string(Address address) => address.Value;

    public static explicit operator Address(string value) => new(value);
}
