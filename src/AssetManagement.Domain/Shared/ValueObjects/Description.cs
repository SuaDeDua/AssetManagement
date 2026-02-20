namespace AssetManagement.Domain.Shared.ValueObjects;

public sealed record Description
{
    const int MaxLength = 500;

    public string Value { get; }

    public Description(string value)
    {
        if (value.Length > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value ?? string.Empty;
    }

    public override string ToString() => Value;

    public static implicit operator string(Description? description) =>
        description?.Value ?? string.Empty;

    public static explicit operator Description(string value) => new(value);
}
