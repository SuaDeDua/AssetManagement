namespace AssetManagement.Domain.Assets.ValueObjects;

// DT - HN or Desktop - HN or Display - HN or Mouse - HCM
// AssetName = CategoryName + ParentLocationCode that mean city
public sealed record AssetName
{
    public string Value { get; }

    public AssetName(string value)
    {
        if()
            throw new
        Value = value;
    }
}
