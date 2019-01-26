using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ControllersAndActions.Controllers
{
    // NOTE: Apply attribute [Microsoft.AspNetCore.Mvc.NonController] for POCO which ends on
    //       "Controller" and are not controllers.
    //       Apply [Microsoft.AspNetCore.Mvc.NonAction] on public methods of the controller to
    //       identify them as simple pulbic methods, not actions
    public class PocoController
    {
        public string Index() => "This is a POCO controller";

        public ViewResult Index2() => new ViewResult()
        {
            ViewName = "Result",
            ViewData = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = $"This is a POCO controller"
            }
        };
    }
}
