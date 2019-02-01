using System.Diagnostics;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Infrastructure
{
    // NOTE: TimeFilter class implements the filter interfaces directly, rather than deriving from 
    //       the convenience attribute class. Filters that rely on dependency injection are applied 
    //       through a different attribute and are not used to decorate controllers or actions
    //       directly.
    public class TimeFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private readonly ConcurrentQueue<double> _actionTimes = new ConcurrentQueue<double>();
        private readonly ConcurrentQueue<double> _resultTimes = new ConcurrentQueue<double>();
        private readonly IFilterDiagnostics _diagnostics;

        public TimeFilter(IFilterDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var timer = Stopwatch.StartNew();
            await next();
            timer.Stop();
            _actionTimes.Enqueue(timer.Elapsed.TotalMilliseconds);
            _diagnostics.AddMessage($"Action time: {timer.Elapsed.TotalMilliseconds} "
                + $"Average: {_actionTimes.Average():F2}");
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var timer = Stopwatch.StartNew();
            await next();
            timer.Stop();
            _resultTimes.Enqueue(timer.Elapsed.TotalMilliseconds);
            _diagnostics.AddMessage($"Result time: {timer.Elapsed.TotalMilliseconds} "
                + $"Average: {_resultTimes.Average():F2}");
        }
    }
}
