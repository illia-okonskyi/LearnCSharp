using Microsoft.AspNetCore.Mvc;
using ModelConventionsAndActionConstrains.Models;
using ModelConventionsAndActionConstrains.Infrastrucure;

namespace ModelConventionsAndActionConstrains.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });

        [ActionName("Details")]
        public IActionResult List() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(List)
        });

        [ActionNamePrefix("Do")]
        public IActionResult PrefixedAction() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(PrefixedAction)
        });

        public IActionResult ActionConstraintsExample() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(ActionConstraintsExample)
        });

        [ActionName("ActionConstraintsExample")]
        [UserAgent("Chrome")]
        public IActionResult ActionConstraintsExample2() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(ActionConstraintsExample2)
        });

        public IActionResult ActionConstraintsWithDiExample() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(ActionConstraintsWithDiExample)
        });

        [ActionName("ActionConstraintsWithDiExample")]
        [UserAgentWithDi("Chrome")]
        public IActionResult ActionConstraintsWithDiExample2() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(ActionConstraintsWithDiExample2)
        });
    }
}
