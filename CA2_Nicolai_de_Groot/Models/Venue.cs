namespace CA2_Nicolai_de_Groot.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Capacity { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
