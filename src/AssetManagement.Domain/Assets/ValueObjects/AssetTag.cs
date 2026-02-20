using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.Assets.ValueObjects;

// CategoryCode + Number tăng dần và duy nhất không được trùng
public sealed record AssetTag
{
    public string Value { get; }

    public AssetTag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyAssetTagException();

        Value = value;
    }

    public static AssetTag FromSequence(CategoryCode code, int sequence) =>
        new($"{code.Value}-{sequence:D4}");
}
