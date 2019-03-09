using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Identity.Models;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public ViewResult Index() => View(GetData(nameof(Index)));

        [Authorize(Roles = "Users")]
        public ViewResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

        private Dictionary<string, object> GetData(string actionName) =>
            new Dictionary<string, object>
            {
                ["Action"] = actionName,
                ["User"] = HttpContext.User.Identity.Name,
                ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
                ["In Users Role"] = HttpContext.User.IsInRole("Users"),
                ["City"] = CurrentUser.Result.City,
                ["Qualification"] = CurrentUser.Result.Qualification
            };

        [Authorize]
        public async Task<IActionResult> EditCustomUserProperties()
        {
            return View(await CurrentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditCustomUserProperties(
            [Required] City city,
            [Required] Qualification qualification)
        {
            if (!ModelState.IsValid)
            {
                return View(await CurrentUser);
            }

            var user = await CurrentUser;
            user.City = city;
            user.Qualification = qualification;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private Task<AppUser> CurrentUser =>
            _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
    }
}
