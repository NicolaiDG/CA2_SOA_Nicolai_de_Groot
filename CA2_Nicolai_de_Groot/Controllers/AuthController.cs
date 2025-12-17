using CA2_Nicolai_de_Groot.Data;
using CA2_Nicolai_de_Groot.Models;
using CA2_Nicolai_de_Groot.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CA2_Nicolai_de_Groot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;


        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message = "User successfully registered",
                userId = user.Id
            });
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.UserName,
                request.Password,
                false,
                false
            );

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var user = await _userManager.FindByNameAsync(request.UserName);

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id
            });
        }

        // GET: api/Auth/Users
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Load all users into memory
            var users = _userManager.Users.ToList();

            var userList = new List<object>();

            foreach (var user in users)
            {
                // Load tickets for the user
                var tickets = await _context.Tickets
                    .Where(t => t.UserId == user.Id)
                    .Select(t => new
                    {
                        t.Id,
                        t.Status,
                        t.PurchasedAt,
                        Event = new
                        {
                            t.Event.Id,
                            t.Event.Title
                        }
                    })
                    .ToListAsync();

                userList.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Tickets = tickets
                });
            }

            return Ok(userList);
        }


        // DELETE: api/Auth/User/{id}
        [HttpDelete("User/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound($"No user found with ID {id}");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                message = "User deleted successfully",
                deletedUserId = id
            });
        }
    }
}
