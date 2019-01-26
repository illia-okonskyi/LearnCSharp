using System;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        public ViewResult ReceiveForm(string name, string city) =>
            View("Result", $"{name} lives in {city}");

        public ViewResult ViewBagUsing()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View("Result");
        }

        // NOTE: Each *Redirect method can has Permanent suffix which produces 301 status code
        //       (permanent redirect, which can be cached by the browsers). Otherwise 302 temporary
        //       redirect is produced.
        // WARNING: You can not specify nameof(DerivedController) as controller paramenter for,
        //          RedirectToAction because MVC will search DerivedControllerController class and
        //          there is no suchcontroller
        public RedirectResult RedirectExample() => Redirect("https://www.google.com");
        public LocalRedirectResult LocalRedirectExample() => LocalRedirect("/Home/Index");
        public RedirectToRouteResult RedirectToRouteExample() =>
            RedirectToRoute(new
            {
                controller = "Home",
                action = "ReceiveForm",
                name = "Alice",
                city = "London"
            });
        public RedirectToActionResult RedirectToActionExample() =>
            RedirectToAction(nameof(DerivedController.Headers), "Derived");

        public JsonResult JsonResultExample()
            => Json(new
            {
                stringValue = "stringValue",
                arrayValue = new[] { "Alice", "Bob", "Joe" }
            });

        public ContentResult ContentResultExample()
            => Content("[\"Alice\",\"Bob\",\"Joe\"]", "application/json");

        public ObjectResult ObjectResultExample() =>
            Ok(new
            {
                stringValue = "stringValue",
                arrayValue = new[] { "Alice", "Bob", "Joe" }
            });

        // NOTE: There are four IActionResult which can be used to return files
        //       1) FileContentResult (accessed via File method of the MVC Controller)
        //          This action result sends a byte array to the client with a specified MIME type.
        //       2) FileStreamResult (accessed via File method of the MVC Controller)
        //          This action result reads a stream and sends the content to the client.
        //       3) VirtualFileResult (accessed via File method of the MVC Controller)
        //          This action result reads a stream from a virtual path (relative to the
        //          application on the host).
        //       4) PhysicalFileResult (accessed via PhysicalFile method of the MVC Controller)
        //          This action result reads the contents of a file from a specified path and sends
        //          the contents to the client.
        public VirtualFileResult FileResultExample()
            => File("/lib/twitter-bootstrap/css/bootstrap.css", "text/css");
    }
}
