using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    // NOTE: Main idea is to implement Post-Redirect-Get pattern to avoid situations of mutliply
    //       posting forms and other data which are sent via POST request.
    //       Example of such case: user fills form, submits it and action method returns view.
    //       Than user clicks "Reload" button of the browser and browser posts data once more.
    // NOTE: TempData which is used to share data across requests relies on the Session service
    public class PostRedirectGetPatternController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        [HttpPost]
        public RedirectToActionResult ReceiveForm(string name, string city)
        {
            TempData["name"] = name;
            TempData["city"] = city;
            return RedirectToAction(nameof(ShowResult));
        }

        public ViewResult ShowResult()
        {
            string name = TempData["name"] as string;
            string city = TempData["city"] as string;
            return View("Result", $"{name} lives in {city}");
        }
    }
}
