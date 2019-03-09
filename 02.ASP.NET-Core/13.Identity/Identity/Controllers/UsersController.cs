using System.Threading.Tasks;
//using System.Security.Claims;
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
        public IActionResult AccessDenied()
        {
            return View();
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

        //[AllowAnonymous]
        //public IActionResult GoogleLogin(string returnUrl)
        //{
        //    var redirectUrl = Url.Action("GoogleResponse", "Users", new { ReturnUrl = returnUrl });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(
        //        "Google", redirectUrl);
        //    // Some magic is here, ASP.NET automatically redirects user to the Google login page
        //    // after user is successfully logged in at Google page - it redirects user back to the
        //    // `redirectUrl` passed to the `properties`
        //    return new ChallengeResult("Google", properties);
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        //{
        //    // User is successfully logged in at Google page

        //    ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    // Try login with claims obtained from external source
        //    var result = await _signInManager.ExternalLoginSignInAsync(
        //        info.LoginProvider, info.ProviderKey, false);
        //    if (result.Succeeded)
        //    {
        //        return Redirect(returnUrl);
        //    }

        //    // If failed - we must create new user and log it in
        //    var user = new AppUser
        //    {
        //        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
        //        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
        //    };
        //    var createUserResult = await _userManager.CreateAsync(user);
        //    if (!createUserResult.Succeeded)
        //    {
        //        return AccessDenied();
        //    }

        //    // AddLoginAsync adds login info from external source to the user
        //    var addLoginResult = await _userManager.AddLoginAsync(user, info);
        //    if (!addLoginResult.Succeeded)
        //    {
        //        return AccessDenied();
        //    }

        //    // SignInAsync signs in the user without checks and returns void
        //    await _signInManager.SignInAsync(user, false);
        //    return Redirect(returnUrl);
        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
