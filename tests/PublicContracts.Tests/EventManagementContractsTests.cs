using AcmeTickets.PublicContracts.Events.EventManagement.Commands;
using AcmeTickets.PublicContracts.Events.EventManagement.Messages;
using FluentAssertions;

namespace PublicContracts.Tests;

public class EventManagementContractsTests
{
    [Fact]
    public void RetrieveTicketGroupIdWithOrderId_Command_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var command = new RetrieveTicketGroupIdWithOrderId();

        // Assert
        command.Should().NotBeNull();
        command.MarketplaceId.Should().Be(0);
        command.MarketplaceOrderKey.Should().Be(Guid.Empty);
        command.EventellectOrderId.Should().Be(Guid.Empty);
    }

    [Fact]
    public void RetrieveTicketGroupIdWithOrderId_Command_ShouldSetProperties()
    {
        // Arrange
        var marketplaceId = 123;
        var marketplaceOrderKey = Guid.NewGuid();
        var eventellectOrderId = Guid.NewGuid();

        // Act
        var command = new RetrieveTicketGroupIdWithOrderId
        {
            MarketplaceId = marketplaceId,
            MarketplaceOrderKey = marketplaceOrderKey,
            EventellectOrderId = eventellectOrderId
        };

        // Assert
        command.MarketplaceId.Should().Be(marketplaceId);
        command.MarketplaceOrderKey.Should().Be(marketplaceOrderKey);
        command.EventellectOrderId.Should().Be(eventellectOrderId);
    }

    [Fact]
    public void RetrieveTicketGroupIdWithOrderIdResponse_Message_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var response = new RetrieveTicketGroupIdWithOrderIdResponse();

        // Assert
        response.Should().NotBeNull();
        response.MarketplaceId.Should().Be(0);
        response.MarketplaceOrderKey.Should().Be(Guid.Empty);
        response.EventellectOrderId.Should().Be(Guid.Empty);
    }

    [Fact]
    public void RetrieveTicketGroupIdWithOrderIdResponse_Message_ShouldSetProperties()
    {
        // Arrange
        var marketplaceId = 456;
        var marketplaceOrderKey = Guid.NewGuid();
        var eventellectOrderId = Guid.NewGuid();

        // Act
        var response = new RetrieveTicketGroupIdWithOrderIdResponse
        {
            MarketplaceId = marketplaceId,
            MarketplaceOrderKey = marketplaceOrderKey,
            EventellectOrderId = eventellectOrderId
        };

        // Assert
        response.MarketplaceId.Should().Be(marketplaceId);
        response.MarketplaceOrderKey.Should().Be(marketplaceOrderKey);
        response.EventellectOrderId.Should().Be(eventellectOrderId);
    }
}
