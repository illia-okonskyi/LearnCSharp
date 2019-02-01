using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class ProfileAttribute : ActionFilterAttribute
    {
        private Stopwatch _timer;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            var result = "<div>Elapsed time: "
                + $"{_timer.Elapsed.TotalMilliseconds} ms</div>";
            var bytes = Encoding.ASCII.GetBytes(result);
            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
        }
    }
}
