using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Validation.Models;

namespace Validation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() =>
            View("MakeBooking", new Appointment { Date = DateTime.Now });

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt)
        {
            if (ModelState.GetValidationState(nameof(appt.Date)) == ModelValidationState.Valid &&
                DateTime.Now > appt.Date)
            {
                // Property-level error message
                ModelState.AddModelError(nameof(appt.Date), "Please enter a date in the future");
            }
            if (ModelState.GetValidationState(nameof(appt.Date)) == ModelValidationState.Valid &&
                ModelState.GetValidationState(nameof(appt.ClientName)) == ModelValidationState.Valid &&
                appt.ClientName.Equals("Joe", StringComparison.OrdinalIgnoreCase) &&
                appt.Date.DayOfWeek == DayOfWeek.Monday)
            {
                // Model-level error message
                ModelState.AddModelError("", "Joe cannot book appointments on Mondays");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View("Completed", appt);
        }
    }
}
