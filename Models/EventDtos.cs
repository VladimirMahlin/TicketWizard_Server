using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal BasePrice { get; set; }
        public int AvailableTickets { get; set; }
    }

    public class EventCreateDto
    {
        [Required] [MaxLength(255)] public string Name { get; set; }

        public string Description { get; set; }

        [Required] public DateTime Date { get; set; }

        [Required] [MaxLength(255)] public string Location { get; set; }

        [Range(0.01, double.MaxValue)] public decimal BasePrice { get; set; }

        [Range(0, int.MaxValue)] public int AvailableTickets { get; set; }
    }

    public class EventUpdateDto
    {
        [Required] public int Id { get; set; }

        [MaxLength(255)] public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(255)] public string Location { get; set; }

        [Range(0.01, double.MaxValue)] public decimal? BasePrice { get; set; }

        [Range(0, int.MaxValue)] public int? AvailableTickets { get; set; }
    }
}