using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Data.Entities;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(255)]
    public string Location { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal BasePrice { get; set; }

    [Range(0, int.MaxValue)]
    public int AvailableTickets { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}