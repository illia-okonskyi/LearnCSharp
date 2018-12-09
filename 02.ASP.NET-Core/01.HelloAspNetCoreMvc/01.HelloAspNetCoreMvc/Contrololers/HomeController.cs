using Microsoft.AspNetCore.Mvc;

namespace HelloAspNetCoreMvc.Contrololers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(Models.Data.GetHelloWorldSet());
    }
}