using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            IdentitySeedData.EnsurePopulated(_userManager).Wait();
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl) => View(new LoginModel { ReturnUrl = returnUrl });

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid && (await Login(loginModel.Name, loginModel.Password)))
            {
                return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        private async Task<bool> Login(string userName, string password)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            await _signInManager.SignOutAsync();
            return (await _signInManager.PasswordSignInAsync(user, password, false, false)).Succeeded;
        }
    }
}
