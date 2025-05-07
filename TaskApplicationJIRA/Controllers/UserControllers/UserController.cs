using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Models.ChangePasswordModel;
using TaskApplicationJIRA.Models.UserModel;
using TaskApplicationJIRA.Services.UserServices;

namespace TaskApplicationJIRA.Controllers.UserControllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUserAsync(user);
                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToAction("Index", "Admin");
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(id, user);
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("Index", "Admin");
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction("Index", "Admin");
        }

        // GET: User/ChangePassword/5
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null) return NotFound();

            var model = new ChangePasswordViewModel
            {
                UserId = user.UserId
            };

            return View(model);
        }

        // POST: User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.ChangePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
            if (result)
            {
                TempData["SuccessMessage"] = "Password changed successfully!";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Current password is incorrect.");
            return View(model);
        }
    }
}
