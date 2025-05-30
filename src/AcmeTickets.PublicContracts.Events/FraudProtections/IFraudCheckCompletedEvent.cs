namespace AcmeTickets.PublicContracts.Events.FraudProtections
{
    public interface IFraudCheckCompletedEvent : AcmeTickets.PublicContracts.Events.IEvent
    {
        string TransactionId { get; }
        bool IsFraudulent { get; }
    }
}
