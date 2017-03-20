using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using _383_Phase1_InventoryTracker.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using _383_Phase1_InventoryTracker.Validation;
using Microsoft.AspNetCore.Authorization;

namespace _383_Phase1_InventoryTracker
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim("Role", "Admin");
                });
                options.AddPolicy("User", policy =>
                {
                    policy.RequireClaim("Role", "User");
                });
                
                    options.DefaultPolicy = new AuthorizationPolicyBuilder("MyCookieMiddlewareInstance").RequireAuthenticatedUser().Build();
               

                //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
                //options.AddPolicy("UserOnly", policy => policy.RequireClaim("User"));

                //options.AddPolicy("ReadPolicy", policyBuilder =>
                //{
                //    policyBuilder.RequireAuthenticatedUser().RequireAssertion(context => context.User.HasClaim("HasAccess", "true")).Build();
                //});
            });



            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();

            var connection = @"Server=(LocalDb)\MSSQLLocalDB;Database= Inventory.db;Trusted_Connection=True;";
            services.AddDbContext<InventoryTrackerContext>(options =>
                   options.UseSqlServer(connection));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, InventoryTrackerContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();
            

            //Configure Cookie Middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
               // AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                AuthenticationScheme = "MyCookieMiddlewareInstance",
                LoginPath = new PathString("/Users/SignIn"),
                AccessDeniedPath = new PathString("/Users/Unauthorized"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,

                Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = Validator.ValidateAsync
                }
            });




            app.UseMvc(builder =>
                {
                    builder.MapRoute("default", "{controller=Home}/{action=index}/{id?}");
                });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);
        }
    }
}
