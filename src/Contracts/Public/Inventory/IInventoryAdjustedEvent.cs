namespace AcmeTickets.PublicContracts.Events.Inventory
{
    public interface IInventoryAdjustedEvent : AcmeTickets.PublicContracts.Events.IEvent
    {
        string ProductId { get; }
        int Quantity { get; }
    }
}
