using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });

        // NOTE: Route attribute overrides the default routing scheme and provides
        //       full URL to the action. This action no longer accessible through
        //       /Home/CustomVariable URL but it can be accessed via next URLs:
        //       - /app/Home/actions/CustomVariable
        //       - /app/Home/actions/CustomVariable/mon
        //      [controller] placeholder is replaced with the name of the target Controller: Home
        //      [action] placeholder is replaced with the name of the target Action: CustomVariable
        [Route("app/[controller]/actions/[action]/{id:weekday?}")]
        public ViewResult CustomVariable(string id, string routeVar)
        {
            Result r = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariable),
            };
            r.Data["id"] = id ?? "<no value>";
            r.Data["routeVar"] = routeVar ?? "<no value>";
            r.Data["GeneratingUrlInCode"] = Url.Action("CustomVariable", "Home", new { id = "fri" });
            return View("Result", r);
        }
    }
}
