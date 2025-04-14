using server.Data.Entities;

namespace server.Data.Repositories.Interfaces;

public interface IEventRepository
{
    Task<Event> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task AddAsync(Event entity);
    Task UpdateAsync(Event entity);
    Task DeleteAsync(int id);
}
