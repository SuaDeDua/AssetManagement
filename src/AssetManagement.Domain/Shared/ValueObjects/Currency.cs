namespace AssetManagement.Domain.Shared.ValueObjects;

public record Currency
{
    private static readonly HashSet<string> ValidCurrencyCodes = ["USD", "VND"];

    public string Code { get; }
    public string Name { get; }
    public string Symbol { get; }

    // Private constructor for EF Core
    private Currency() { }

    public Currency(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code cannot be null or empty", nameof(code));

        code = code.ToUpperInvariant();

        if (!IsValidCurrencyCode(code))
            throw new ArgumentException($"Invalid currency code: {code}", nameof(code));

        Code = code;
        Name = GetCurrencyName(code);
        Symbol = GetCurrencySymbol(code);
    }

    public static bool IsValidCurrencyCode(string code)
    {
        return !string.IsNullOrWhiteSpace(code)
            && ValidCurrencyCodes.Contains(code.ToUpperInvariant());
    }

    public static Currency VND => new("VND");
    public static Currency USD => new("USD");

    public static int DecimalPlaces => 2;

    private static string GetCurrencyName(string code) =>
        code switch
        {
            "VND" => "Việt Nam đồng",
            "USD" => "US Dollar",
            _ => code,
        };

    private static string GetCurrencySymbol(string code) =>
        code switch
        {
            "VND" => "₫",
            "USD" => "$",
            _ => code,
        };

    public override string ToString() => Code;

    public static implicit operator string(Currency currency) => currency.Code;

    public static explicit operator Currency(string code) => new(code);
}
