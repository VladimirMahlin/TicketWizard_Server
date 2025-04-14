using server.Models;

namespace server.Services.Interfaces;

public interface IEventService
{
    Task<EventDto> GetEventByIdAsync(int id);
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto> CreateEventAsync(EventCreateDto eventCreateDto);
    Task<bool> UpdateEventAsync(EventUpdateDto eventUpdateDto);
    Task<bool> DeleteEventAsync(int id);
}