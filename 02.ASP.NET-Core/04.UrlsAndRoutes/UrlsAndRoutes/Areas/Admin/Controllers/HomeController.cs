using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Areas.Admin.Models;

namespace UrlsAndRoutes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Person[] _persons = new Person[] {
            new Person { Name = "Alice", City = "London" },
            new Person { Name = "Bob", City = "Paris" },
            new Person { Name = "Joe", City = "New York" }
        };

        public ViewResult Index() => View(_persons);

        [Route("[area]/app/[controller]/actions/[action]")]
        // Matches /Admin/app/Home/actions/AttributeRoute
        public ViewResult AttributeRoute() => View(nameof(Index), _persons);
    }
}
