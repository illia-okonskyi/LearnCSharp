using System;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        public ViewResult ReceiveForm(string name, string city) =>
            View("Result", $"{name} lives in {city}");

        public ViewResult ViewBagUsing()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View("Result");
        }

        // NOTE: Each *Redirect method can has Permanent suffix which produces 301 status code
        //       (permanent redirect, which can be cached by the browsers). Otherwise 302 temporary
        //       redirect is produced.
        // WARNING: You can not specify nameof(DerivedController) as controller paramenter for,
        //          RedirectToAction because MVC will search DerivedControllerController class and
        //          there is no suchcontroller
        public RedirectResult RedirectExample() => Redirect("https://www.google.com");
        public LocalRedirectResult LocalRedirectExample() => LocalRedirect("/Home/Index");
        public RedirectToRouteResult RedirectToRouteExample() => 
            RedirectToRoute(new {
                controller = "Home",
                action = "ReceiveForm",
                name = "Alice",
                city = "London"
            });
        public RedirectToActionResult RedirectToActionExample() =>
            RedirectToAction(nameof(DerivedController.Headers), "Derived");
    }
}
