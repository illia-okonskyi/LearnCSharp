using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConfiguringApps.Infrastructure;
using ConfiguringApps.Infrastructure.Middleware;

namespace ConfiguringApps
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ResponseEditingMiddleware>();
            app.UseMiddleware<RequestEditingMiddleware>();
            app.UseMiddleware<ShortCircuitMiddleware>();
            app.UseMiddleware<ContentMiddleware>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
