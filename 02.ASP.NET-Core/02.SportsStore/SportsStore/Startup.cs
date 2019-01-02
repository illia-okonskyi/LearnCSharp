﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                _configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, DbProductRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 });
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 });
                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
