using CA2_Nicolai_de_Groot.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CA2_Nicolai_de_Groot.Pages
{
    public class VenuesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VenuesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Venue> Venues { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var result = await client.GetFromJsonAsync<List<Venue>>(
                "https://localhost:7119/api/Venues");

            if (result != null)
            {
                Venues = result;
            }
        }
    }
}
