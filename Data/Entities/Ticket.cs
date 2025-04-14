using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Data.Entities;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Event")]
    public int EventId { get; set; }
    public Event Event { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public bool IsReserved { get; set; }

    [ForeignKey("Order")]
    public int? OrderId { get; set; }
    public Order Order { get; set; }
}