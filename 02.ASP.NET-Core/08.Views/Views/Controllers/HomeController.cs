using Microsoft.AspNetCore.Mvc;
using System;

namespace Views.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View(new string[] { "Apple", "Orange", "Pear" });

        public ViewResult List() => View();
    }
}
