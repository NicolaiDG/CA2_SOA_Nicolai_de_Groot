namespace CA2_Nicolai_de_Groot.Pages.ViewModels
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? EventDescription { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }

        public string VenueName { get; set; } = null!;
    }
}
