using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCA.Data;
using System.Collections.Generic;
using System.Globalization;

namespace RCA
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

            //adicionando o serviço de cookies na aplicação
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Hone/Login");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<RCAContext>(options => options.UseMySql(Configuration.GetConnectionString("RCAConection"), builder => builder.MigrationsAssembly("RCA")));

            services.AddScoped<RCAService>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RCAService _Service)
        {
            //globalization
            var _enUS = new CultureInfo("en-US");
            var _locOPTs = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(_enUS),
                SupportedCultures = new List<CultureInfo> { _enUS },
                SupportedUICultures = new List<CultureInfo> { _enUS }
            };
            app.UseRequestLocalization(_locOPTs);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _Service.Populated();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
