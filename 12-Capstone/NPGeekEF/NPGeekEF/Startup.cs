using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NPGeekEF.Models;
using NPGeekEF.DAL;
using System;
using TE.AuthLib;
using TE.AuthLib.DAL;
using Microsoft.AspNetCore.Mvc;

namespace NPGeekEF
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
            // Configure cookie policy
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            }
            );

            // Add dependency injection for DbContext and DAOs
            services.AddDbContext<NpGeekContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<IParksDAO, ParksEFCoreDAO>();
            services.AddTransient<IWeatherDAO, WeatherEFCoreDAO>();
            services.AddTransient<ISurveyDAO, SurveyEFCoreDAO>();

            // Add dependency injection for User Authentication
            services.AddTransient<IUserDAO, UserSqlDAO>(p => new UserSqlDAO(Configuration.GetConnectionString("User")));
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthProvider, SessionAuthProvider>();
           
            // Additional setup for TEAuthLib
            AuthorizeAttribute.Options = new AuthorizeAttribute.AuthorizeAttributeOptions()
            {
                LoginRedirectAction = "Login",
                LoginRedirectController = "Account"
            };

            // Globally add Anti Forgery Token requirement
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            }
            );

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
