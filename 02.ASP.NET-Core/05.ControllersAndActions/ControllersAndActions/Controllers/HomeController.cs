using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        public ViewResult ReceiveForm(string name, string city)
        {
            // NOTE: Values can be obtained in a diffrerent way:
            //       var name = Request.Form["name"];
            //       var city = Request.Form["city"];
            return View("Result", $"{name} lives in {city}");
        }
    }
}
