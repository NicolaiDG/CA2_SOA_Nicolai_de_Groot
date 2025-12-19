using CA2_Nicolai_de_Groot.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CA2_Nicolai_de_Groot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        private string UserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost("{eventId}")]
        public async Task<IActionResult> AddFavorite(int eventId)
        {
            bool exists = await _context.FavoriteEvents
                .AnyAsync(f => f.EventId == eventId && f.Auth0UserId == UserId);

            if (!exists)
            {
                _context.FavoriteEvents.Add(new FavoriteEvent
                {
                    EventId = eventId,
                    Auth0UserId = UserId
                });

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _context.FavoriteEvents
                .Include(f => f.Event)
                .Where(f => f.Auth0UserId == UserId)
                .Select(f => f.Event)
                .ToListAsync();

            return Ok(favorites);
        }
    }
}
