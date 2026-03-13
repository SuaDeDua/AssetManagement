namespace AssetManagement.Domain.Locations.ValueObjects;

public record CityCode
{
    public string Value { get; }

    private CityCode() { }

    public CityCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(value);

        Value = value;
    }
}
