using System.Diagnostics;

namespace ConfiguringApps.Infrastructure
{
    public class UptimeService
    {
        private readonly Stopwatch _timer;

        public UptimeService()
        {
            _timer = Stopwatch.StartNew();
        }

        public long Uptime => _timer.ElapsedMilliseconds;
    }
}