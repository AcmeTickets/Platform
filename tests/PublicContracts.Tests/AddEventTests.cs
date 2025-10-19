using AcmeTickets.Contracts.Public.Platform.Commands;
using FluentAssertions;

namespace PublicContracts.Tests;

public class AddEventTests
{
    [Fact]
    public void AddEvent_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var addEvent = new AddEvent();

        // Assert
        addEvent.EventId.Should().Be(Guid.Empty);
        addEvent.EventName.Should().BeNull();
        addEvent.EventDate.Should().Be(default(DateTime));
    }

    [Fact]
    public void AddEvent_ShouldSetProperties()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventName = "Rock Concert 2025";
        var eventDate = new DateTime(2025, 12, 31, 20, 0, 0);

        // Act
        var addEvent = new AddEvent
        {
            EventId = eventId,
            EventName = eventName,
            EventDate = eventDate
        };

        // Assert
        addEvent.EventId.Should().Be(eventId);
        addEvent.EventName.Should().Be(eventName);
        addEvent.EventDate.Should().Be(eventDate);
    }

    [Fact]
    public void AddEvent_EventName_ShouldAcceptNull()
    {
        // Arrange & Act
        var addEvent = new AddEvent
        {
            EventId = Guid.NewGuid(),
            EventName = null,
            EventDate = DateTime.Now
        };

        // Assert
        addEvent.EventName.Should().BeNull();
    }

    [Fact]
    public void AddEvent_EventDate_ShouldHandlePastDates()
    {
        // Arrange
        var pastDate = new DateTime(2020, 1, 1);

        // Act
        var addEvent = new AddEvent
        {
            EventId = Guid.NewGuid(),
            EventName = "Past Event",
            EventDate = pastDate
        };

        // Assert
        addEvent.EventDate.Should().Be(pastDate);
    }

    [Fact]
    public void AddEvent_EventDate_ShouldHandleFutureDates()
    {
        // Arrange
        var futureDate = new DateTime(2030, 12, 31);

        // Act
        var addEvent = new AddEvent
        {
            EventId = Guid.NewGuid(),
            EventName = "Future Event",
            EventDate = futureDate
        };

        // Assert
        addEvent.EventDate.Should().Be(futureDate);
    }
}
