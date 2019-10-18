using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace CodeFirst
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var conectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<EfDbContext>(options => {
                options.UseSqlServer(conectionString);
            });
            services.AddTransient<IProductsRepository, EfProductsRepository>();

            string customerConString = Configuration["ConnectionStrings:CustomerConnection"];
            services.AddDbContext<EfCustomerContext>(options => {
                options.UseSqlServer(customerConString);
            });
            services.AddTransient<ICustomerRepository, EfCustomerRepository>();
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
