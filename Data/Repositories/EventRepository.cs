using Microsoft.EntityFrameworkCore;
using server.Data.Entities;
using server.Data.Repositories.Interfaces;

namespace server.Data.Repositories;

public class EventRepository(AppDbContext dbContext) : IEventRepository
{
    public async Task<Event> GetByIdAsync(int id)
    {
        return await dbContext.Events.FindAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await dbContext.Events.ToListAsync();
    }

    public async Task AddAsync(Event entity)
    {
        await dbContext.Events.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventToDelete = await dbContext.Events.FindAsync(id);
        if (eventToDelete != null)
        {
            dbContext.Events.Remove(eventToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}