using System.Text.Json;
using AcmeTickets.Contracts.Public.Platform.Commands;
using FluentAssertions;

namespace PublicContracts.Tests;

public class ContractSerializationTests
{
    [Fact]
    public void AddEvent_ShouldSerializeToJson()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var addEvent = new AddEvent
        {
            EventId = eventId,
            EventName = "Test Event",
            EventDate = new DateTime(2025, 12, 31, 20, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var json = JsonSerializer.Serialize(addEvent);

        // Assert
        json.Should().NotBeNullOrEmpty();
        json.Should().Contain(eventId.ToString());
        json.Should().Contain("Test Event");
    }

    [Fact]
    public void AddEvent_ShouldDeserializeFromJson()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventDate = new DateTime(2025, 12, 31, 20, 0, 0, DateTimeKind.Utc);
        var json = $$"""
        {
            "EventId": "{{eventId}}",
            "EventName": "Test Event",
            "EventDate": "{{eventDate:O}}"
        }
        """;

        // Act
        var addEvent = JsonSerializer.Deserialize<AddEvent>(json);

        // Assert
        addEvent.Should().NotBeNull();
        addEvent!.EventId.Should().Be(eventId);
        addEvent.EventName.Should().Be("Test Event");
        addEvent.EventDate.Should().Be(eventDate);
    }

    [Fact]
    public void AddEvent_ShouldHandleNullEventNameInSerialization()
    {
        // Arrange
        var addEvent = new AddEvent
        {
            EventId = Guid.NewGuid(),
            EventName = null,
            EventDate = DateTime.UtcNow
        };

        // Act
        var json = JsonSerializer.Serialize(addEvent);
        var deserialized = JsonSerializer.Deserialize<AddEvent>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.EventName.Should().BeNull();
    }

    [Fact]
    public void AddEvent_RoundTrip_ShouldPreserveAllProperties()
    {
        // Arrange
        var original = new AddEvent
        {
            EventId = Guid.NewGuid(),
            EventName = "Round Trip Test",
            EventDate = new DateTime(2025, 6, 15, 14, 30, 0, DateTimeKind.Utc)
        };

        // Act
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<AddEvent>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.EventId.Should().Be(original.EventId);
        deserialized.EventName.Should().Be(original.EventName);
        deserialized.EventDate.Should().Be(original.EventDate);
    }
}
