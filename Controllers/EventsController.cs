using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventService eventService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
    {
        var events = await eventService.GetAllEventsAsync();
        return Ok(events.Select(e => new EventDto
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Date = e.Date,
            Location = e.Location,
            BasePrice = e.BasePrice,
            AvailableTickets = e.AvailableTickets
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var @event = await eventService.GetEventByIdAsync(id);

        return Ok(new EventDto
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            Date = @event.Date,
            Location = @event.Location,
            BasePrice = @event.BasePrice,
            AvailableTickets = @event.AvailableTickets
        });
    }

    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateEvent(EventCreateDto eventCreateDto)
    {
        var @event = await eventService.CreateEventAsync(eventCreateDto);
        return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, new EventDto
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            Date = @event.Date,
            Location = @event.Location,
            BasePrice = @event.BasePrice,
            AvailableTickets = @event.AvailableTickets
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, EventUpdateDto eventUpdateDto)
    {
        if (id != eventUpdateDto.Id)
        {
            return BadRequest();
        }

        var result = await eventService.UpdateEventAsync(eventUpdateDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var result = await eventService.DeleteEventAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}