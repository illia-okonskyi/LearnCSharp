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
    }
}
