using AssetManagement.Domain.Shared.Exceptions;

namespace AssetManagement.Domain.Assets.Exceptions;

public sealed class AssetException : DomainException
{
    public AssetException()
        : base() { }
}

public sealed class EmptyAssetTagException : DomainException
{
    public EmptyAssetTagException()
        : base("Asset tag cannot be empty.") { }
}

public sealed class InvalidCategoryCodeException : DomainException
{
    public InvalidCategoryCodeException(string code)
        : base($"Category code {code} is invalid. It must be 2-4 characters.") { }

    public InvalidCategoryCodeException()
        : base() { }
}

public sealed class InvalidSerialNumberException : DomainException
{
    public string SerialNumber { get; }

    public InvalidSerialNumberException(string serialNumber)
        : base($"Serial number {serialNumber} is invalid. It must be 10-30 characters.")
    {
        SerialNumber = serialNumber;
    }

    public InvalidSerialNumberException()
        : base() { }
}

public sealed class AssetTagGenerationFailedException : DomainException
{
    public AssetTagGenerationFailedException()
        : base("Failed to generate a unique asset tag.") { }
}
