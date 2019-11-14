using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Routing_20.Infrastructure;

namespace Routing_20
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "crawler-robot",
                    template: "robots.txt",
                    defaults: new { Controller = "Crawler", Action = "Robots" });

                //Error and statuscode route
                routes.MapRoute(
                    name: "Error",
                    template: "{culture}/error/{action}/{statuscode?}",
                    defaults: new { Controller = "Error" });

                //url:"","sv-se"
                //Actions on home controller
                routes.MapRoute(
                   name: "domain_actions",
                   template: "{culture?}/",
                   defaults: new { controller = "Home", Action = "index" },
                   constraints: new { });

                //Note that home is not a valid controller...
                //url:"sv-se/admin/login","sv-se/admin/"
                routes.MapRoute(
                   name: "culture",
                   template: "{culture}/{controller=home}/{action=index}/{id?}",
                   defaults:null,
                   constraints: new { controller = new IsValidController() });


                //url:"sv-se/content/127-article"
                routes.MapRoute(
                   name: "home_actions",
                   template: "{culture}/{action}/{id?}",
                   defaults: new { controller = "Home" },
                   constraints: new {  });

            });
        }
    }
}
