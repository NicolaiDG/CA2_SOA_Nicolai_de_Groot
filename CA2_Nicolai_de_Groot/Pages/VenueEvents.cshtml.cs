using CA2_Nicolai_de_Groot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CA2_Nicolai_de_Groot.Pages
{
    public class VenueEventsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VenueEventsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Venue Venue { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var venues = await client.GetFromJsonAsync<List<Venue>>(
                "https://localhost:7119/api/Venues");

            if (venues == null)
                return NotFound();

            var venue = venues.FirstOrDefault(v => v.Id == id);

            if (venue == null)
                return NotFound();

            Venue = venue;
            return Page();
        }
    }
}
