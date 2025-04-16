using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController(ITicketService ticketService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);
            return Ok(ticket);
        }

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByEvent(int eventId)
        {
            var tickets = await ticketService.GetTicketsByEventIdAsync(eventId);
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket(TicketCreateDto ticketCreateDto)
        {
            var ticket = await ticketService.CreateTicketAsync(ticketCreateDto);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketUpdateDto ticketUpdateDto)
        {
            if (id != ticketUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await ticketService.UpdateTicketAsync(ticketUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await ticketService.DeleteTicketAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}