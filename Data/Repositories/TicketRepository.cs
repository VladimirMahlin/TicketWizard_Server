using Microsoft.EntityFrameworkCore;
using server.Data.Entities;
using server.Data.Repositories.Interfaces;

namespace server.Data.Repositories;

public class TicketRepository(AppDbContext dbContext) : ITicketRepository
{
    public async Task<Ticket> GetByIdAsync(int id)
    {
        return await dbContext.Tickets.FindAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await dbContext.Tickets.ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByEventIdAsync(int eventId)
    {
        return await dbContext.Tickets.Where(t => t.EventId == eventId).ToListAsync();
    }

    public async Task AddAsync(Ticket entity)
    {
        await dbContext.Tickets.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ticket entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ticketToDelete = await dbContext.Tickets.FindAsync(id);
        if (ticketToDelete != null)
        {
            dbContext.Tickets.Remove(ticketToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}