using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskApplicationJIRA.Models.UserModel;
using TaskApplicationJIRA.Services.AccountServices;
using TaskApplicationJIRA.Models.ChangePasswordModel;

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
                TempData["Error"] = "User already exists with this email.";
                return View(model);
            }

            TempData["LoginSuccess"] = "Registration successful. You can now log in.";
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
                TempData["Error"] = "Invalid email or password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["LoginSuccess"] = user.Role switch
            {
                "Admin" => "Welcome back, Admin!",
                "Scrum Master" => "Welcome Scrum Master!",
                "Developer" => "Welcome Developer!",
                _ => "Login successful!"
            };

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
            TempData["LoginSuccess"] = "You have logged out successfully.";
            return RedirectToAction("Login", "Account");
        }
        // GET: ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(); // Return the change password view
        }


        // POST: ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return with validation errors if invalid
            }

            // Get the currently authenticated user's email
            var email = User?.Identity?.Name;

            if (email == null)
            {
                TempData["Error"] = "You must be logged in to change your password.";
                return RedirectToAction("Login", "Account");
            }

            // Call the service method to change the password
            var success = await _accountService.ChangePasswordAsync(email, model.CurrentPassword, model.NewPassword);

            if (!success)
            {
                TempData["Error"] = "The current password is incorrect, or something went wrong.";
                return View(model); // Return to the form with an error message
            }

            TempData["Success"] = "Your password has been changed successfully!";
            return RedirectToAction("Index", "Home"); // Redirect to a success page or the home page
        }

    }
}
