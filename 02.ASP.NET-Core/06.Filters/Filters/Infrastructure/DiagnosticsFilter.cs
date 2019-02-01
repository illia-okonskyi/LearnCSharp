using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    // NOTE: DiagnosticsFilter class implements the filter interface directly, rather than deriving
    //       from the convenience attribute class. Filters that rely on dependency injection are
    //       applied  through a different attribute and are not used to decorate controllers or
    //       actions directly.
    public class DiagnosticsFilter : IAsyncResultFilter
    {
        private readonly IFilterDiagnostics _diagnostics;

        public DiagnosticsFilter(IFilterDiagnostics diags)
        {
            _diagnostics = diags;
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            await next();

            foreach (var message in _diagnostics?.Messages)
            {
                var bytes = Encoding.ASCII.GetBytes($"<div>{message}</div>");
                await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
