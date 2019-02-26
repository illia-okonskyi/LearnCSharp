using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() =>
            View(new Dictionary<string, object> {
                ["Placeholder"] = "Placeholder"
            });
    }
}
