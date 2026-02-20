using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.Assets.ValueObjects;

public sealed record SerialNumber
{
    public string Value { get; }

    public SerialNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidSerialNumberException(value);

        Value = value;
    }

    public static SerialNumber Create(string? existingSn = null)
    {
        return !string.IsNullOrWhiteSpace(existingSn)
            ? new SerialNumber(existingSn)
            : GenerateNew();
    }

    public static SerialNumber GenerateNew()
    {
        string now = DateTime.UtcNow.ToString("yyMMdd");
        string timeStamp = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 10000).ToString("D4");
        string randomString = Guid.NewGuid().ToString("N")[..4].ToUpper();

        return new SerialNumber($"SN{now}{timeStamp}{randomString}");
    }

    public override string ToString() => Value;
}
