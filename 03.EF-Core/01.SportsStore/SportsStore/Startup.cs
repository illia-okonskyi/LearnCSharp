using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IRepository, MemoryRepository>();
            services.AddTransient<IRepository, DbRepository>();
            services.AddTransient<ICategoryRepository, DbCategoryRepository>();
            services.AddTransient<IOrdersRepository, DbOrdersRepository>();
            services.AddTransient<IWebServiceRepository, DbWebServiceRepository>();

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<AppDbContext>(options => {
                // Use this to force logging the query arguments and other sensitive data
                // during development
                options.EnableSensitiveDataLogging();

                options.UseSqlServer(connectionString);
            });

            // SQL-cache coomand-line:
            // dotnet sql-cache create "Server=(localdb)\MSSQLLocalDB;Database=SportsStore" "dbo" "SessionData"
            services.AddDistributedSqlServerCache(options => {
                options.ConnectionString = connectionString;
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });
            services.AddSession(options => {
                options.Cookie.Name = "SportsStore.Session";
                options.IdleTimeout = System.TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}
