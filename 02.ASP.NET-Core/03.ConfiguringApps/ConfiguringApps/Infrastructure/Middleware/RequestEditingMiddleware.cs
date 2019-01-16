using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure.Middleware
{
    public class RequestEditingMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public RequestEditingMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EdgeBrowser"] = httpContext.Request.Headers["User-Agent"].Any(v => v.ToLower().Contains("edge"));
            await _nextDelegate.Invoke(httpContext);
        }
    }
}
