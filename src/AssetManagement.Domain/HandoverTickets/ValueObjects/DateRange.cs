using AssetManagement.Domain.Assets.Exceptions;

namespace AssetManagement.Domain.HandoverTickets.ValueObjects;

public record DateRange
{
    private DateRange() { }

    public DateOnly Start { get; init; }

    public DateOnly End { get; init; }

    public int LengthInDays => End.DayNumber - Start.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new AssetException("End date precedes start date");
        }

        return new DateRange { Start = start, End = end };
    }
}
