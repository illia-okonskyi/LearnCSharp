using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure.Middleware
{
    public class ResponseEditingMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ResponseEditingMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _nextDelegate.Invoke(httpContext);

            if (httpContext.Response.StatusCode != 200)
            {
                await httpContext.Response.WriteAsync($"Status code {httpContext.Response.StatusCode}", Encoding.UTF8);
            }
        }
    }
}
