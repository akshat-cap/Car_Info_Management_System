using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using car1.Models;
using Microsoft.EntityFrameworkCore;
using car1.Data;
using car1.Auth;
using System.Linq;
using System.Threading.Tasks;

namespace car1.Controllers
{
    public class LoginController : Controller
    {
        private readonly CarinfomanagementContext _context;

        public LoginController(CarinfomanagementContext context)
        {
            _context = context;
        }

        // GET: /Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] Login collection)
        {
            try
            {
                // Fetch the user from the database based on the username
                var user = await _context.Logins
                    .Include(u => u.UserType) // Eager load the associated UserType
                    .FirstOrDefaultAsync(u => u.Username == collection.Username);

                if (user == null || user.Password != collection.Password) // Check if the user exists and the password is correct
                {
                    return BadRequest("Invalid username or password.");
                }

                // Role-based mapping logic using the UserType from the database
                List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Role", user.UserType.type) // Get the role from the UserType
                };

                // Generate JWT Token
                string token = GenerateToken.TokenGenerator("QW5hbmR2YXJtYXRlc3R2YXNybWEuYWpka2RrYWpramRmYWRzZg==", roles, 100);

                // Save token in session and set the header
                HttpContext.Session.SetString("JwtToken", $"Bearer {token}");
                HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

                // Redirect to the home page after login
                ViewBag.Token = token;
                return Redirect("/Cars/Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
