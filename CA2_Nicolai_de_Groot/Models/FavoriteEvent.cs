using CA2_Nicolai_de_Groot.Models;

public class FavoriteEvent
{
    public int Id { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public string Auth0UserId { get; set; } = null!;
}
