using HelloNetCore.Identities;
using HelloNetCore.Models;
using HelloNetCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HelloNetCore
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void options(CookieAuthenticationOptions options)
        {
            options.LoginPath = "/Security/Login";
            options.LogoutPath = "/Security/Logout";
            options.AccessDeniedPath = "/Security/AccessDenied";
            options.SlidingExpiration = true;
               options.Cookie = new CookieBuilder
               {
                   HttpOnly = true,
                   Name = "AspNetCoreDemo.Security.Cookie",
                   Path = "/",
                   SameSite = SameSiteMode.Lax,
                   SecurePolicy = CookieSecurePolicy.SameAsRequest
               };
        }

        public void ConfigureServices(IServiceCollection services)
        {
   
            services.AddMvc().AddRazorRuntimeCompilation(); // required to run BrowserLink middleware
           
            //related with dependency injection.
            services.AddScoped<ICalculator, Calculator18>(); // We can change Calculator18 another ICaltulator types.
            //services.AddSingleton<ICalculator, Calculator18>();
            //services.AddTransient<ICalculator, Calculator18>();

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options);

            //var connection = @"Server = (localdb)\MSSQLLocalDB; Database=SchoolDb; Integrated Security = True;";
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(_configuration["dbConnection"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration["dbConnection"]));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = env.EnvironmentName = "Pruduction";  // EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink(); //adding BrowserLink middleware.
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(ConfigureRoute);
            app.UseStaticFiles();
            #region Route e.g.
            //app.UseEndpoints(endpoints =>
            //{

            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=home}/{action=index}/{id?}"
            //    );

            //endpoints.MapControllerRoute(
            //     name: "first",
            //     pattern: "{controller}"
            // );



            //automatically created by visual studio
            //endpoints.MapGet("/default", async context =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //}); 
            #endregion
        }

        private void ConfigureRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            endpointRouteBuilder.MapControllerRoute("myroute", "kaan/{controller=home}/{action=index3}/{id?}");
            endpointRouteBuilder.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            endpointRouteBuilder.MapRazorPages();
        }

    }
}
