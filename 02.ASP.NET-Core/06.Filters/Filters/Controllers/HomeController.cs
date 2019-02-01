using Microsoft.AspNetCore.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    // NOTE: Filters are applied as attributes to whole controller or to concrete action
    //       To apply filter to all actions of all controllers use global filter

    [HttpsOnly]
    public class HomeController : Controller
    {
        public ViewResult Index() =>
            View("Message", "This is the Index action on the Home controller");

        public ViewResult SecondAction() =>
            View("Message", "This is the SecondAction action on the Home controller");

        [Profile]
        public ViewResult Profiled() => View("Message", "Profiled action");

        [ProfileAsync]
        public ViewResult ProfiledAsync() => View("Message", "ProfiledAsync action");

        [ViewResultDetails]
        public ViewResult ViewResultDetails() => View("Message", "ViewResultDetails action");

        [ViewResultDetailsAsync]
        public ViewResult ViewResultDetailsAsync() => View("Message", "ViewResultDetailsAsync action");

        [HybridProfile]
        [ViewResultDetailsAsync]
        public ViewResult HybridProfiled() => View("Message", "HybridProfiled action");
    }
}
