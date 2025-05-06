using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskApplicationJIRA.Models.UserModel;
using TaskApplicationJIRA.Services.AccountServices;

namespace TaskApplicationJIRA.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _accountService.RegisterUserAsync(model);

            if (!success)
            {
                TempData["Error"] = "User already exists with this email."; // SweetAlert Error Message
                return View(model);
            }

            TempData["LoginSuccess"] = "Registration successful. You can now log in."; // SweetAlert Success Message
            return RedirectToAction("Login", "Account");
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _accountService.AuthenticateAsync(email, password);

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password."; // SweetAlert Error Message
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username), // Stores the user's display name or username. Useful for displaying in the UI or identifying the logged-in user
                new Claim(ClaimTypes.Email, user.Email),   //Stores the email address, which might be used for contacting or uniquely identifying the user.
                new Claim(ClaimTypes.Role, user.Role)      //Stores the user role (e.g., Admin, Developer, Scrum Master). This is crucial for role-based authorization, allowing you to restrict access to specific parts of the app.
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["LoginSuccess"] = "Login successful!"; // SweetAlert Success Message

            return user.Role switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Scrum Master" => RedirectToAction("Index", "ScrumMaster"),
                "Developer" => RedirectToAction("Index", "Developer"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // POST: Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["LoginSuccess"] = "You have logged out successfully."; // SweetAlert Success Message
            return RedirectToAction("Login", "Account");
        }
    }
}
