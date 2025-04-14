using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<TicketDto>? Tickets { get; set; }
}

public class OrderCreateDto
{
    [Required] public List<int> TicketIds { get; set; }
}

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<int> TicketIds { get; set; }
    public string Message { get; set; }
}

public class OrderUpdateDto
{
    [Required] public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public List<int> TicketIds { get; set; }
}