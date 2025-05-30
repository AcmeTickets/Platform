namespace AcmeTickets.PublicContracts.Events.EventManagement
{
    public interface ITicketRequestedEvent : AcmeTickets.PublicContracts.Events.IEvent
    {
        string TicketId { get; }
        string UserId { get; }
    }
}
