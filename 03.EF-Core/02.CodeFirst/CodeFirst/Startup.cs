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
            services.AddTransient<ISupplierRepository, EfSupplierRepository>();
            services.AddTransient<IGenericRepository<ContactDetails>, EfGenericRepository<ContactDetails>>();
            services.AddTransient<IGenericRepository<ContactLocation>, EfGenericRepository<ContactLocation>>();

            services.AddTransient<MigrationsManager>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            EfDbContext productContext,
            EfCustomerContext customerContext)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                SeedData.Seed(productContext);
                SeedData.Seed(customerContext);
            }
        }
    }
}
