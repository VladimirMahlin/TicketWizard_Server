using server.Models;

namespace server.Services.Interfaces;

public interface ITicketService
{
    Task<TicketDto> GetTicketByIdAsync(int id);
    Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
    Task<IEnumerable<TicketDto>> GetTicketsByEventIdAsync(int eventId);
    Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketCreateDto);
    Task<bool> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    Task<bool> DeleteTicketAsync(int id);
}