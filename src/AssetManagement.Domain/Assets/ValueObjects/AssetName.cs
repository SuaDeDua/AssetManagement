using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.Assets.ValueObjects;

// DT - HN or Desktop - HN or Display - HN or Mouse - HCM
// AssetName = CategoryName + City
public sealed record AssetName
{
    public string Value { get; }

    public AssetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyAssetNameException();

        Value = value;
    }

    public static AssetName Create(string categoryName, string city)
    {
        ValidateInputs(categoryName, city);

        string generateName = FormatName(categoryName, city);

        return new AssetName(generateName);
    }

    private static void ValidateInputs(string categoryName, string city)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            throw new AssetException("Category name is required to generate asset name.");

        if (string.IsNullOrWhiteSpace(city))
            throw new AssetException("City is required to generate asset name.");
    }

    private static string FormatName(string categoryName, string city)
    {
        return $"{categoryName.Trim()} - ({city.Trim()})";
    }

    public static implicit operator string(AssetName name) => name.Value;
}
