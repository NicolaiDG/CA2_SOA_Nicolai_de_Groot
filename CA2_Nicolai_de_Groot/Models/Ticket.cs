using System.Text.Json.Serialization;

namespace CA2_Nicolai_de_Groot.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        // Foreign keys
        public int EventId { get; set; }
        public string UserId { get; set; } = null!;

        // Navigation properties - must be ignored to avoid cycles
        [JsonIgnore]
        public Event? Event { get; set; }

        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        // Extra properties
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        // Possible values: Active, Cancelled, Refunded, etc.
        public string Status { get; set; } = "Active";
    }
}
