using Microsoft.EntityFrameworkCore;
using server.Data.Entities;
using server.Data.Repositories.Interfaces;

namespace server.Data.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int id)
    {
        return await dbContext.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await dbContext.Orders.ToListAsync();
    }

    public async Task AddAsync(Order entity)
    {
        await dbContext.Orders.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderToDelete = await dbContext.Orders.FindAsync(id);
        if (orderToDelete != null)
        {
            dbContext.Orders.Remove(orderToDelete);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<Order> GetOrderWithTicketsAsync(int orderId)
    {
        return await dbContext.Orders.Include(o => o.Tickets).FirstOrDefaultAsync(o => o.Id == orderId);
    }
}