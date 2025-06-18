namespace AcmeTickets.PublicContracts.Events.EventManagement.Messages
{
    public class RetrieveTicketGroupIdWithOrderIdResponse
    {
        public int MarketplaceId { get; set; }
        public Guid MarketplaceOrderKey { get; set; }
        public Guid EventellectOrderId { get; set; }
    }
}
