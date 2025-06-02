using NServiceBus;
using Microsoft.AspNetCore.Mvc;
using AcmeTickets.Platform.API.Commands;
using AcmeTickets.Contracts.Public.Platform.Commands;

namespace AcmeTickets.Platform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMessageSession _session;
        public EventController(IMessageSession session)
        {
            _session = session;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEventWithTickets addEventWithTickets)
        {
            var ticketRows = "ZABCDEFGHIJKLMNOPQRS".ToCharArray();
            var random = new Random();

            var sendOptions = new SendOptions();
            sendOptions.SetDestination("EventManagement.Message");
            var addEvent = new AddEvent
            {
                EventName = addEventWithTickets.EventName,
                EventDate = addEventWithTickets.EventDate,
                EventId = addEventWithTickets.EventId
            };
            await _session.Send(addEvent, sendOptions);

            // var ticketGroup = new AddTicketGroupToInventory
            // {
            //     EventId = addEvent.EventId,
            // };

          
            // sendOptions = new SendOptions();
            // sendOptions.SetDestination("ASBTriggerInventory");

            // for (int i = 1; i < 10; i++)
            // {
            //     var numberOftickets = random.Next(4, 10);
            //     ticketGroup.Tickets = new List<Tickets>();
            //     for (int j = 1; j < numberOftickets; j++)
            //     {
            //         ticketGroup.Tickets.Add(new Tickets() { Row = ticketRows[i].ToString(), Seat = j, TicketId = Guid.NewGuid() });
            //     }
            //     await _session.Send(ticketGroup, sendOptions);
            // }
            return new OkObjectResult($"{nameof(AddEvent)} sent. {addEvent.EventId}");

        }
    }
}
