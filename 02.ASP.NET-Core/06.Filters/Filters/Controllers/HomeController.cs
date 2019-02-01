using System;
using Microsoft.AspNetCore.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    // NOTE: Filters are applied as attributes to whole controller or to concrete action
    //       To apply filter to all actions of all controllers use global filter
    // NOTE: Filters execute order can be changed via IOrderedFilter.Order property (custom filters 
    //       should implement this interface too for enabling the ordering feature). Example:
    //       [Message("Default MVC Message filter with custom order", Order = 10)]
    //       The [TypeFilter] and [ServiceFilter] attributes also implement the IOrderedFilter
    //       interface, which means you can change the filter order when using dependency injection
    //       as well.

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

        [RangeException]
        public ViewResult GenerateException(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return View("Message", $"The value is {id}");
        }

        [TypeFilter(typeof(DiagnosticsFilter))]
        [ServiceFilter(typeof(TimeFilter))]
        public ViewResult DependencyInjectionInFilters()
            => View("Message", "DependencyInjectionInFilters action");
    }
}
