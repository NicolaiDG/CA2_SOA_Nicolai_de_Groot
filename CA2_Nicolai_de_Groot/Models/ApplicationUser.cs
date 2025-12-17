using Microsoft.AspNetCore.Identity;

namespace CA2_Nicolai_de_Groot.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
