using server.Data.Entities;
using server.Data.Repositories.Interfaces;
using server.Models;
using server.Services.Interfaces;

namespace server.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<EventDto> GetEventByIdAsync(int id)
    {
        var @event = await eventRepository.GetByIdAsync(id);
        return MapToDto(@event);
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await eventRepository.GetAllAsync();
        return events.Select(MapToDto);
    }

    public async Task<EventDto> CreateEventAsync(EventCreateDto eventCreateDto)
    {
        var newEvent = new Event
        {
            Name = eventCreateDto.Name,
            Description = eventCreateDto.Description,
            Date = eventCreateDto.Date,
            Location = eventCreateDto.Location,
            BasePrice = eventCreateDto.BasePrice,
            AvailableTickets = eventCreateDto.AvailableTickets
        };

        await eventRepository.AddAsync(newEvent);
        return MapToDto(newEvent);
    }

    public async Task<bool> UpdateEventAsync(EventUpdateDto eventUpdateDto)
    {
        var existingEvent = await eventRepository.GetByIdAsync(eventUpdateDto.Id);

        existingEvent.Name = eventUpdateDto.Name;
        existingEvent.Description = eventUpdateDto.Description;

        if (eventUpdateDto.Date.HasValue)
        {
            existingEvent.Date = eventUpdateDto.Date.Value;
        }

        existingEvent.Location = eventUpdateDto.Location;

        if (eventUpdateDto.BasePrice.HasValue)
        {
            existingEvent.BasePrice = eventUpdateDto.BasePrice.Value;
        }

        if (eventUpdateDto.AvailableTickets.HasValue)
        {
            existingEvent.AvailableTickets = eventUpdateDto.AvailableTickets.Value;
        }

        await eventRepository.UpdateAsync(existingEvent);
        return true;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        await eventRepository.DeleteAsync(id);
        return true;
    }

    private EventDto MapToDto(Event @event)
    {
        return new EventDto
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            Date = @event.Date,
            Location = @event.Location,
            BasePrice = @event.BasePrice,
            AvailableTickets = @event.AvailableTickets
        };
    }
}