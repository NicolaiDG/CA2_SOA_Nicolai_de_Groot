using CA2_Nicolai_de_Groot.Models;
using CA2_Nicolai_de_Groot.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CA2_Nicolai_de_Groot.Pages
{
    public class EventsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<EventViewModel> Events { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var events = await client.GetFromJsonAsync<List<Event>>(
                "https://localhost:7119/api/Events");
            var venues = await client.GetFromJsonAsync<List<Venue>>(
                "https://localhost:7119/api/Venues");

            if (events == null || venues == null)
                return;

            Events = events.Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                VenueName = venues.First(v => v.Id == e.VenueId).Name
            }).ToList();
        }
    }
}
