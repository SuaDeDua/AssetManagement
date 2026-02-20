using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.Assets.ValueObjects;

public sealed record CategoryCode
{
    public string Value { get; }

    public CategoryCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 4)
            throw new InvalidCategoryCodeException(nameof(value));

        Value = value.ToUpper();
    }

    public override string ToString() => Value;

    public static implicit operator string(CategoryCode categoryCode) => categoryCode.Value;

    public static explicit operator CategoryCode(string value) => new(value);
}
