using HelloNetCore.Models;
using HelloNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation(); // required to run BrowserLink middleware

            //related with dependency injection.
            services.AddScoped<ICalculator, Calculator18>(); // We can change Calculator18 another ICaltulator types.
            //services.AddSingleton<ICalculator, Calculator18>();
            //services.AddTransient<ICalculator, Calculator18>();

            var connection = @"Server = (localdb)\MSSQLLocalDB; Database=SchoolDb; Integrated Security = True;";
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink(); //adding BrowserLink middleware.
            }
            app.UseRouting();
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
            app.UseEndpoints(ConfigureRoute);


        }

        private void ConfigureRoute(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            endpointRouteBuilder.MapControllerRoute("MyRoute", "kaan/{controller=home}/{action=index3}/{id?}");
            //endpointRouteBuilder.MapRazorPages();
        }

    }
}
