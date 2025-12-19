using CA2_Nicolai_de_Groot.Models;
using CA2_Nicolai_de_Groot.Pages.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CA2_Nicolai_de_Groot.Pages
{
    public class EventDetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventDetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public EventDetailsViewModel Event { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var events = await client.GetFromJsonAsync<List<Event>>(
                "https://localhost:7119/api/Events");

            var venues = await client.GetFromJsonAsync<List<Venue>>(
                "https://localhost:7119/api/Venues");

            if (events == null || venues == null)
                return NotFound();

            var ev = events.FirstOrDefault(e => e.Id == id);
            if (ev == null)
                return NotFound();

            Event = new EventDetailsViewModel
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDescription = ev.EventDescription,
                StartTime = ev.StartTime,
                EndTime = ev.EndTime,
                BasePrice = ev.BasePrice,
                VenueName = venues.First(v => v.Id == ev.VenueId).Name
            };

            return Page();
        }

        public async Task<IActionResult> OnPostFavoriteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(
                $"https://localhost:7119/api/Favorites/{id}",
                null);

            response.EnsureSuccessStatusCode();

            return RedirectToPage(new { id });
        }

    }
}
