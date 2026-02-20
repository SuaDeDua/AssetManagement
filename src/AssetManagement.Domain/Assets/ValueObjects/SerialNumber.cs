using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.Assets.ValueObjects;

public sealed record SerialNumber
{
    public string Value { get; }

    public SerialNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 10 || value.Length > 30)
            throw new InvalidSerialNumberException(nameof(value));

        Value = value;
    }

    private SerialNumber() { }

    public static SerialNumber GenerateAuto()
    {
        string datePart = GetDateTime();
        string timePart = GetTimesStamp();
        string randomPart = GetRandomString();
        string finalValue = CombineParts(datePart, timePart, randomPart);

        return new SerialNumber(finalValue.ToUpper());
    }

    private static string GetDateTime()
    {
        var now = DateTime.UtcNow;
        return now.ToString("yyMMdd");
    }

    private static string GetTimesStamp()
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string timePart = (timestamp % 10000).ToString("D4");

        return timePart.ToString();
    }

    private static string GetRandomString()
    {
        return Guid.NewGuid().ToString()[..4].ToUpper();
    }

    private static string CombineParts(string datePart, string timePart, string randomPart)
    {
        return $"SN-{datePart}-{timePart}-{randomPart}";
    }

    public override string ToString() => Value;

    public static implicit operator string(SerialNumber serialNumber) => serialNumber.Value;

    public static explicit operator SerialNumber(string value) => new(value);
}
