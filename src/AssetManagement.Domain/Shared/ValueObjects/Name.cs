namespace AssetManagement.Domain.Shared.ValueObjects;

public sealed record Name
{
    const int MaxLength = 50;

    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }
}
