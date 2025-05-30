namespace AcmeTickets.PublicContracts.Events.Fulfillment
{
    public interface IFulfillmentCompletedEvent : AcmeTickets.PublicContracts.Events.IEvent
    {
        string OrderId { get; }
        bool Success { get; }
    }
}
