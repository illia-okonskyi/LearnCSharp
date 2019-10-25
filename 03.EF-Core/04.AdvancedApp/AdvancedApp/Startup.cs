using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AdvancedApp.Models;

namespace AdvancedApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // NOTE: warning messages can be configured for service via WarningsConfigurationBuilder:
            //       - Ignore(event) - This method tells Entity Framework Core to ignore the
            //                         specified event.
            //       - Log(event) - This method tells Entity Framework Core to log the specified
            //                      event.
            //       - Throw(event) - This method tells Entity Framework Core to throw an exception
            //                        for the specified event.
            //      Complete list of the events is available at
            //      https://docs.microsoft.com/en-us/ef/core/api/microsoft.entityframeworkcore.infrastructure.relationaleventid
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<AdvancedContext>(options =>
                options.UseSqlServer(connectionString).ConfigureWarnings(warning =>
                    warning.Throw(RelationalEventId.QueryClientEvaluationWarning))
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
