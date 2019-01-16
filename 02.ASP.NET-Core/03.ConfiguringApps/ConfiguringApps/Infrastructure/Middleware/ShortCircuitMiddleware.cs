using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure.Middleware
{
    public class ShortCircuitMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Items["EdgeBrowser"] as bool? == true)
            {
                httpContext.Response.StatusCode = 403;
            }
            else
            {
                await _nextDelegate.Invoke(httpContext);
            }
        }
    }
}