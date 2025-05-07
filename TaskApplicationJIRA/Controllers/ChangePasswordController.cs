using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskApplicationJIRA.Models.ChangePasswordModel;
using TaskApplicationJIRA.Services.AccountServices;

namespace TaskApplicationJIRA.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly IAccountService _accountService;

        public ChangePasswordController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // ✅ GET: ChangePassword
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);
            var user = await _accountService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            var model = new ChangePasswordViewModel
            {
                UserId = user.UserId
            };

            return View(model);
        }

        // ✅ POST: ChangePassword
        [HttpPost]
        public async Task<IActionResult> Index(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return Unauthorized();

            var result = await _accountService.ChangePasswordAsync(email, model.CurrentPassword, model.NewPassword);

            if (!result)
            {
                TempData["Error"] = "Current password is incorrect.";
                return View(model);
            }

            TempData["Success"] = "Password changed successfully.";
            return RedirectToAction("Login", "Account");
        }
    }
}
