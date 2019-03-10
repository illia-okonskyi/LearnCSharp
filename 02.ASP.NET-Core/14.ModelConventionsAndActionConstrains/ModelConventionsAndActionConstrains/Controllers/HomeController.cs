using Microsoft.AspNetCore.Mvc;
using ModelConventionsAndActionConstrains.Models;

namespace ModelConventionsAndActionConstrains.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });

        public IActionResult List() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(List)
        });
    }
}
