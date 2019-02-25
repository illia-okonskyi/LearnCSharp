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
            if (string.IsNullOrEmpty(appt.ClientName))
            {
                // Property-level error message
                ModelState.AddModelError(nameof(appt.ClientName), "Please enter your name");
            }
            if (ModelState.GetValidationState("Date") == ModelValidationState.Valid &&
                DateTime.Now > appt.Date)
            {
                // Property-level error message
                ModelState.AddModelError(nameof(appt.Date), "Please enter a date in the future");
            }
            if (!appt.TermsAccepted)
            {
                // Property-level error message
                ModelState.AddModelError(nameof(appt.TermsAccepted), "You must accept the terms");
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
