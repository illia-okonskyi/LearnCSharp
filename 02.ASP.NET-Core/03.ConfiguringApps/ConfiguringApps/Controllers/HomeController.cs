using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConfiguringApps.Infrastructure;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private readonly UptimeService _uptime;

        public HomeController(UptimeService uptime)
        {
            _uptime = uptime;
        }

        public ViewResult Index() => View(new Dictionary<string, string>
        {
            ["Message"] = "This is the Index action",
            ["Uptime"] = $"{_uptime.Uptime} ms"
        });
    }
}