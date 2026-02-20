using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class OrderInformation : Entity<Guid>
{
    public int Warranty { get; private set; }
    public string? OrderNumber { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public string? Supplier { get; private set; }
    public Money PurchaseCost { get; private set; }
}
