using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        public ViewResult Index() => View("Result", $"This is a derived controller");

        public ViewResult Headers()
        {
            return View("DictionaryResult",
                Request.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First()));
        }
         
    }
}
