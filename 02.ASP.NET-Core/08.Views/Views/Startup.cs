using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Views.Infrastructure;

namespace Views
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcViewOptions>(options => {
                // NOTE: You can call options.ViewEngines.Clear() before adding custom view engine
                //       to ensure that no other view engines are registered
                options.ViewEngines.Insert(0, new CustomViewEngine());
            });
            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new CustomViewLocationExpander());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
