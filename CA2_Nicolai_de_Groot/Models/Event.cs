using System.Text.Json.Serialization;

namespace CA2_Nicolai_de_Groot.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }

        public int VenueId { get; set; }

        [JsonIgnore]   
        public Venue? Venue { get; set; }

        [JsonIgnore]   
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
