using Microsoft.AspNetCore.Mvc;
using System;

namespace Views.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View(new string[] { "Apple", "Orange", "Pear" });

        public ViewResult List() => View();

        public ViewResult CustomViewAndViewEngine()
        {
            ViewBag.Message = "Hello, World";
            ViewBag.Time = DateTime.Now.ToString("HH:mm:ss");
            return View("CustomView");
        }

        public ViewResult CustomViewLocationExpander()
        {
            return View("CustomViewLocationExpander");
        }

    }
}
