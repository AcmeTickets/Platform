namespace AcmeTickets.Platform.API.Commands;

using NServiceBus;


public class AddEventWithTickets
{
   public Guid EventId { get; set; }
   public string? EventName { get; set; }
   public DateTime EventDate { get; set; }
}
