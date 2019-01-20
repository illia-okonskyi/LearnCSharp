using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConfiguringApps.Infrastructure;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private readonly UptimeService _uptime;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UptimeService uptime, ILogger<HomeController> logger)
        {
            _uptime = uptime;
            _logger = logger;
        }

        public ViewResult Index(bool throwException = false)
        {
            _logger.LogDebug($"Handled {Request.Path} at uptime {_uptime.Uptime}");

            if (throwException)
            {
                throw new NullReferenceException();
            }

            return View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{_uptime.Uptime} ms"
            });
        }

        public ViewResult Error()
        {
            return View(nameof(Index), new Dictionary<string, string>
            {
                ["Message"] = "This is the Error action"
            });
        }
    }
}