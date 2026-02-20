using AssetManagement.Domain.Shared.Validation;

namespace AssetManagement.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }

    private Money() { }

    public Money(decimal amount, Currency currency)
    {
        Guard.AgainstNull(currency);
        Guard.AgainstNegative(amount);

        Amount = amount;
        Currency = currency;
    }
}
