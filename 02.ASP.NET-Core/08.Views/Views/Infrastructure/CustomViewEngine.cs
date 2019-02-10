using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Views.Infrastructure
{
    public class CustomViewEngine : IViewEngine
    {
        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return ViewEngineResult.NotFound(viewPath,
            new string[] { "(Debug View Engine - GetView)" });
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (viewName == "CustomView")
            {
                return ViewEngineResult.Found(viewName, new CustomView());
            }
            else
            {
                return ViewEngineResult.NotFound(viewName,
                new string[] { "(Debug View Engine - FindView)" });
            }
        }
    }
}
