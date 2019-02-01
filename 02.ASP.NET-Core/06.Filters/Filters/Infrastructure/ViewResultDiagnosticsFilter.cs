using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class ViewResultDiagnosticsFilter : IActionFilter
    {
        private readonly IFilterDiagnostics _diagnostics;

        public ViewResultDiagnosticsFilter(IFilterDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do nothing - not used in this filter
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult vr)
            {
                _diagnostics.AddMessage($"View name: {vr.ViewName}");
                _diagnostics.AddMessage($@"Model type: {vr.ViewData.Model.GetType().Name}");
            }
        }
    }
}
