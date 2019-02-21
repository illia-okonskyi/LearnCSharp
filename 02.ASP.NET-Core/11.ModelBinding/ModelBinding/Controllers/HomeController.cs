using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var person = _repository[id.Value];
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        public ViewResult Create() => View(new Person());

        [HttpPost]
        public ViewResult Create(Person model) => View("Index", model);


        // NOTE: The Bind attribute can be used to bind some arguments with specified prefix.
        //       Note that only prefix can be specified, the names of the properties must be same
        //       as class has.
        //       BindNever attribute (can be applied to the property) tells MVC do not bind any
        //       value from  the view to the model.
        //       BindRequired attribute that tells the model  binding process that a request must
        //       include a value for a property. If the request doesn’t have a required value, then
        //       a model validation error is produced. Example:
        //
        //public class Models.AddressSummary
        //{
        //    [BindRequired]
        //    public string City { get; set; }
        //    [BindNever]
        //    public string Country { get; set; }
        //}
        //[HttpPost]
        //public ViewResult DisplayAddressSummary(
        //    [Bind(nameof(AddressSummary.City), Prefix = nameof(Person.HomeAddress))] AddressSummary summary)
        //{
        //    return View(summary);
        //}

        public ViewResult Addresses(IList<AddressSummary> addresses) =>
            View(addresses ?? new List<AddressSummary>());

        // NOTE: Specifying model binding source can be done via next attributes:
        //       - FromForm - select form data as the source of binding data.
        //       - FromRoute - select the routing system as the source of binding data.
        //       - FromQuery - select the query string as the source of binding data.
        //       - FromHeader - select a request header as the source of binding data.
        //       - FromBody - specifies that the request body should be used as the source of
        //                    binding data, which is required when you want to receive data from
        //                    requests that are not form-encoded, such as in API controllers.
        public ViewResult HeaderSource(HeadersModel model) => View(model);

        public ViewResult BodySource() => View();

        [HttpPost]
        public Person BodySource([FromBody]Person model) => model;
    }
}
