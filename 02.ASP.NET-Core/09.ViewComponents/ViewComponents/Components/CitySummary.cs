using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.Components
{
    // NOTE: View components derived from ViewComponent can omit the "ViewComponent" prefix
    public class CitySummary : ViewComponent
    {
        private readonly ICityRepository _repository;

        public CitySummary(ICityRepository repository)
        {
            _repository = repository;
        }

        // NOTE: Invoke method can return one of 4 result types:
        //       1,2) string or ContentViewComponentResult (implements IViewComponentResult) -
        //            result string will be safely HTML-encoded, use the Content method of the 
        //            ViewComponent class
        //       3) ViewViewComponentResult (implements IViewComponentResult) - This class is used
        //          to specify a Razor view, with optional view model data. Use the View method of
        //          the ViewComponent class
        //       4) HtmlContentViewComponentResult (implements IViewComponentResult) - This class
        //          is used to specify a fragment of HTML that will be included in the HTML
        //          document without further encoding.There is no ViewComponent method to create
        //          this type of result

        //public string Invoke()
        //{
        //    return $"{_repository.Cities.Count()} cities, "
        //        + $"{_repository.Cities.Sum(c => c.Population)} people";
        //}
        //
        //
        //public IViewComponentResult Invoke()
        //{
        //    return Content("This is a <h3><i>string</i></h3>");
        //}
        // 
        //
        //using Microsoft.AspNetCore.Mvc.ViewComponents;
        //using Microsoft.AspNetCore.Html;
        //public IViewComponentResult Invoke()
        //{
        //    return new HtmlContentViewComponentResult(
        //        new HtmlString("This is a <h3><i>string</i></h3>"));
        //}

        // NOTE: Search locations of the partial views for view components:
        //       - /Views/{controller}/Components/{component}/{viewName} | Default.cshtml
        //       - /Views/Shared/Components/{component}/{viewName} | Default.cshtml
        //       Sharing partial views to view compoments can be made by specifying full path
        //       of the target view like Views/Components/CommonViews/ViewName.cshtml
        public IViewComponentResult Invoke()
        {
            var target = RouteData.Values["id"] as string;
            var cities = _repository.Cities.Where(
                city => target == null || string.Compare(city.Country, target, true) == 0);

            return View(new CityViewModel
            {
                Cities = cities.Count(),
                Population = cities.Sum(c => c.Population)
            }); ;
        }
    }
}
