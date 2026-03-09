using AssetManagement.Domain.HandoverTickets.ValueObjects;
using AssetManagement.Domain.Shared.Common;

namespace AssetManagement.Domain.HandoverTickets;

public sealed class HandoverTicket : AggregateRoot<Guid>
{
    public string TicketNumber { get; private set; }
    public HandoverType Type { get; private set; }
    public HandoverStatus Status { get; private set; }
    public Guid SenderUserId { get; }
    public Guid ReceiverId { get; }
    public DateRange Duration { get; private set; }
}
