using AcmeTickets.Platform.Api.Controllers;
using AcmeTickets.Platform.API.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NServiceBus;

namespace Api.Tests;

public class EventControllerTests
{
    [Fact]
    public async Task Post_ShouldReturnOkResult()
    {
        // Arrange
        var mockMessageSession = new Mock<IMessageSession>();
        mockMessageSession
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<SendOptions>(), default))
            .Returns(Task.CompletedTask);

        var controller = new EventController(mockMessageSession.Object);
        var addEventWithTickets = new AddEventWithTickets
        {
            EventId = Guid.NewGuid(),
            EventName = "Test Concert",
            EventDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var result = await controller.Post(addEventWithTickets);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Post_ShouldSendAddEventCommand()
    {
        // Arrange
        var mockMessageSession = new Mock<IMessageSession>();
        mockMessageSession
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<SendOptions>(), default))
            .Returns(Task.CompletedTask);

        var controller = new EventController(mockMessageSession.Object);
        var eventId = Guid.NewGuid();
        var addEventWithTickets = new AddEventWithTickets
        {
            EventId = eventId,
            EventName = "Test Event",
            EventDate = DateTime.UtcNow.AddDays(60)
        };

        // Act
        await controller.Post(addEventWithTickets);

        // Assert
        mockMessageSession.Verify(
            m => m.Send(
                It.Is<object>(cmd => 
                    cmd.GetType().Name == "AddEvent"),
                It.Is<SendOptions>(opts => 
                    opts.GetDestination() == "EventManagement.Message"),
                default),
            Times.Once);
    }

    [Fact]
    public async Task Post_ShouldReturnMessageWithEventId()
    {
        // Arrange
        var mockMessageSession = new Mock<IMessageSession>();
        mockMessageSession
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<SendOptions>(), default))
            .Returns(Task.CompletedTask);

        var controller = new EventController(mockMessageSession.Object);
        var eventId = Guid.NewGuid();
        var addEventWithTickets = new AddEventWithTickets
        {
            EventId = eventId,
            EventName = "Another Event",
            EventDate = DateTime.UtcNow.AddMonths(1)
        };

        // Act
        var result = await controller.Post(addEventWithTickets) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        result.Value.ToString().Should().Contain(eventId.ToString());
    }

    [Fact]
    public async Task Post_WithNullEventName_ShouldStillWork()
    {
        // Arrange
        var mockMessageSession = new Mock<IMessageSession>();
        mockMessageSession
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<SendOptions>(), default))
            .Returns(Task.CompletedTask);

        var controller = new EventController(mockMessageSession.Object);
        var addEventWithTickets = new AddEventWithTickets
        {
            EventId = Guid.NewGuid(),
            EventName = null,
            EventDate = DateTime.UtcNow
        };

        // Act
        var result = await controller.Post(addEventWithTickets);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        mockMessageSession.Verify(
            m => m.Send(It.IsAny<object>(), It.IsAny<SendOptions>(), default),
            Times.Once);
    }
}
