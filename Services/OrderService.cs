using server.Data.Entities;
using server.Data.Repositories.Interfaces;
using server.Models;
using server.Services.Interfaces;

namespace server.Services;

public class OrderService(IOrderRepository orderRepository, ITicketRepository ticketRepository) : IOrderService
{
    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetOrderWithTicketsAsync(id);
        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
    {
        if (orderCreateDto?.TicketIds == null || !orderCreateDto.TicketIds.Any())
        {
            return null;
        }

        var tickets = new List<Ticket>();
        decimal totalAmount = 0;

        foreach (var ticketId in orderCreateDto.TicketIds)
        {
            var ticket = await ticketRepository.GetByIdAsync(ticketId);
            if (ticket.IsReserved)
            {
                return null;
            }
            ticket.IsReserved = true;
            ticket.OrderId = null;
            tickets.Add(ticket);
            totalAmount += ticket.Price;
            await ticketRepository.UpdateAsync(ticket);
        }

        var newOrder = new Order
        {
            OrderDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Tickets = tickets
        };

        await orderRepository.AddAsync(newOrder);

        foreach (var ticket in newOrder.Tickets)
        {
            ticket.OrderId = newOrder.Id;
            await ticketRepository.UpdateAsync(ticket);
        }

        return MapToDto(newOrder);
    }

    public async Task<bool> UpdateOrderAsync(OrderUpdateDto orderUpdateDto)
    {
        var existingOrder = await orderRepository.GetByIdAsync(orderUpdateDto.Id);
        if (existingOrder == null)
        {
            return false;
        }

        if (orderUpdateDto.OrderDate.HasValue)
        {
            existingOrder.OrderDate = orderUpdateDto.OrderDate.Value;
        }
        if (orderUpdateDto.TotalAmount.HasValue)
        {
            existingOrder.TotalAmount = orderUpdateDto.TotalAmount.Value;
        }
        
        // handling ticket

        await orderRepository.UpdateAsync(existingOrder);
        return true;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var orderToDelete = await orderRepository.GetOrderWithTicketsAsync(id);
        foreach (var ticket in orderToDelete.Tickets)
        {
            ticket.IsReserved = false;
            ticket.OrderId = null;
            await ticketRepository.UpdateAsync(ticket);
        }
        await orderRepository.DeleteAsync(id);
        return true;
    }

    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Tickets = order.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                EventId = t.EventId,
                Price = t.Price,
                IsReserved = t.IsReserved
            }).ToList()
        };
    }
}