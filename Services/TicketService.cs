using server.Data.Entities;
using server.Data.Repositories.Interfaces;
using server.Models;
using server.Services.Interfaces;

namespace server.Services;

public class TicketService(ITicketRepository ticketRepository) : ITicketService
{
    public async Task<TicketDto> GetTicketByIdAsync(int id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        return MapToDto(ticket);
    }

    public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();
        return tickets.Select(MapToDto);
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByEventIdAsync(int eventId)
    {
        var tickets = await ticketRepository.GetByEventIdAsync(eventId);
        return tickets.Select(MapToDto);
    }

    public async Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        var newTicket = new Ticket
        {
            EventId = ticketCreateDto.EventId,
            Price = ticketCreateDto.Price
        };

        await ticketRepository.AddAsync(newTicket);
        return MapToDto(newTicket);
    }

    public async Task<bool> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto)
    {
        var existingTicket = await ticketRepository.GetByIdAsync(ticketUpdateDto.Id);

        if (ticketUpdateDto.Price.HasValue)
        {
            existingTicket.Price = ticketUpdateDto.Price.Value;
        }

        if (ticketUpdateDto.IsReserved.HasValue)
        {
            existingTicket.IsReserved = ticketUpdateDto.IsReserved.Value;
        }

        await ticketRepository.UpdateAsync(existingTicket);
        return true;
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
        await ticketRepository.DeleteAsync(id);
        return true;
    }

    private TicketDto MapToDto(Ticket ticket)
    {
        return new TicketDto
        {
            Id = ticket.Id,
            EventId = ticket.EventId,
            Price = ticket.Price,
            IsReserved = ticket.IsReserved
        };
    }
}