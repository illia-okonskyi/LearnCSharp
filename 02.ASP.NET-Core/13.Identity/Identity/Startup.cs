using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure;
using Identity.Models;

namespace Identity
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
            services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(_configuration["Data:Identity:ConnectionString"]));

            var identityBuilder = services.AddIdentity<AppUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });
            identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();
            identityBuilder.AddDefaultTokenProviders();

            // NOTE: When not authorized user tries to access restricted actions the automatic
            //       redirect result is generated. Default redirection link is
            //       /Account/Login?ReturnUrl=<URL> and it can be changed via 
            //       IServiceCollection.ConfigureApplicationCookie extenstion method
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Users/Login";
                options.AccessDeniedPath = "/Users/AccessDenied";
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            // NOTE: To prevent memory leaks ASP.NET Core performs checks when scoped services are
            //       requested from the global scope (it's like singletone object obtains service
            //       lifetime of which must enclosed within the HTTP request scope and this
            //       causes scoped service will be never disposed). So, for such situations the
            //       explicit scope of the services is needed
            // NOTE: Entity Framework Core doesn’t have integrated support for working with seed
            //       data, and care must be taken when creating migrations to disable seeding. The 
            //       seeding statement can be enabled again once the database migration has been
            //       created and applied.
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                AppIdentityDbContext.EnsureAdminAccountPresent(userManager, roleManager, _configuration)
                    .Wait();
            }
        }
    }
}
