using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure.Middleware
{
    public class ContentMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly UptimeService _uptime;

        public ContentMiddleware(RequestDelegate nextDelegate, UptimeService uptime)
        {
            _nextDelegate = nextDelegate;
            _uptime = uptime;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                var responseContent = "This is from the content middleware" +
                    $"(uptime: {_uptime.Uptime}ms)";
                await httpContext.Response.WriteAsync(responseContent, Encoding.UTF8);
            }
            else
            {
                await _nextDelegate.Invoke(httpContext);
            }
        }
    }
}