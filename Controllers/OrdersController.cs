using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = await orderService.CreateOrderAsync(orderCreateDto);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            if (id != orderUpdateDto.Id)
            {
                return BadRequest("Order ID mismatch.");
            }

            var result = await orderService.UpdateOrderAsync(orderUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}