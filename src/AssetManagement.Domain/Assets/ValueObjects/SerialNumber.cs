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

    private SerialNumber() { }

    public static SerialNumber Create(CategoryCode code, string? existingSn = null)
    {
        return !string.IsNullOrWhiteSpace(existingSn)
            ? new SerialNumber(existingSn)
            : GenerateNew(code);
    }

    public static SerialNumber GenerateNew(CategoryCode code)
    {
        string now = DateTime.UtcNow.ToString("ddMMyy");
        string timeStamp = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 10000).ToString("D4");

        return new SerialNumber($"SN{code.Value}{now}{timeStamp}");
    }

    public override string ToString() => Value;
}
