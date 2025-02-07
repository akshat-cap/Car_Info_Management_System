using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using car1.Auth;
using car1.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using car1.Auth;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace car1.Controllers
{


    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([FromForm] Login collection)
        {
            try
            {
                // Role-based mapping logic
                List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
                if (collection.Username == "admin" && collection.Password == "admin@123")
                {
                    roles.Add(new KeyValuePair<string, string>("Role", "Admin"));
                }
                else if (collection.Username == "user" && collection.Password == "user@123")
                {
                    roles.Add(new KeyValuePair<string, string>("Role", "User"));
                }
              
                else
                {
                    return BadRequest("Invalid username or password.");
                }

                // Generate JWT Token
                string token = GenerateToken.TokenGenerator("QW5hbmR2YXJtYXRlc3R2YXNybWEuYWpka2RrYWpramRmYWRzZg==", roles, 100);

                // Save token in session and set the header
                HttpContext.Session.SetString("JwtToken", $"Bearer {token}");
                HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

                // Redirect to the home page after login
                ViewBag.Token = token;
                return Redirect("/Home/Index");
            }
            catch
            {
                return View();
            }
        }
    }

}
