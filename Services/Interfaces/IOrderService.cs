using server.Models;

namespace server.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
    Task<bool> UpdateOrderAsync(OrderUpdateDto orderUpdateDto);
    Task<bool> DeleteOrderAsync(int id);
}