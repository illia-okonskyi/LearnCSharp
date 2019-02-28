using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Identity.Models;

namespace Identity.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }

            if (!await Login(details.Email, details.Password))
            {
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
                return View(details);
            }

            return Redirect(details.ReturnUrl ?? "/");
        }

        private async Task<bool> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            return result.Succeeded;
        }
    }
}
