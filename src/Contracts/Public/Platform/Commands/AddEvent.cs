namespace AcmeTickets.Contracts.Public.Platform.Commands;

public class AddEvent
{
    public Guid EventId { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
}