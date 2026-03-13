using AssetManagement.Domain.Locations.ValueObjects;

namespace AssetManagement.Domain.HandoverTickets.ValueObjects;

public record TicketNumber
{
    public string Value { get; private set; }

    public TicketNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(value, "Ticket number can not null.");

        Value = value;
    }

    public static TicketNumber Generate(CityCode code)
    {
        string datetime = DateTime.UtcNow.ToString("yyyyMMdd");
        string timeStamp = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 10000).ToString("D4");

        return new TicketNumber($"{datetime}{code.Value}{timeStamp}");
    }
}
