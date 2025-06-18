namespace AcmeTickets.PublicContracts.Events.EventManagement.Commands
{
    public class RetrieveTicketGroupIdWithOrderId
    {
        public int MarketplaceId { get; set; }
        public Guid MarketplaceOrderKey { get; set; }
        public Guid EventellectOrderId { get; set; }
    }
}
