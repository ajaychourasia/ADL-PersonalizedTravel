using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADL.PersonalizedTravel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.CognitiveServices.Personalizer;
using ADL.PersonalizedTravel.Repositories;
using ADL.PersonalizedTravel.Services;
using ADL.PersonalizedTravel.Models;

namespace ADL.PersonalizedTravel
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
            var personalizationEndPoint = Configuration["PersonalizationEndpoint"];
            var personalizationApiKey = Configuration["PersonalizationApiKey"];
            if (string.IsNullOrEmpty(personalizationEndPoint) || string.IsNullOrEmpty(personalizationApiKey))
            {
                ///throw new ArgumentException("Missing Azure Personalizer endpoint and/or api key.");
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IPersonalizerClient>(s => CreateClient(personalizationEndPoint, personalizationApiKey));
            services.AddSingleton<IRankableActionRepository, RankableActionRepository>();
            services.AddSingleton<IPersonalizerService, PersonalizerService>();
            services.AddSingleton<IActionRepository, ActionRepository>();
            services.AddSingleton<ITourRepository, TourRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<AppUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddIdentity<AppUser,IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions aiOptions                = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();            aiOptions.EnableRequestTrackingTelemetryModule = false;            services.AddApplicationInsightsTelemetry(aiOptions);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                 name: "recommendation",
                 template: "{controller=Tour}/{action=Recommendation}");

                routes.MapRoute(
               name: "reward",
               template: "{controller=Tour}/{action=Reward}/{id?}");
            });
        }
        private IPersonalizerClient CreateClient(string uri, string ApiKey)
        {
            return new PersonalizerClient(
                new ApiKeyServiceClientCredentials(ApiKey))
            {
                Endpoint = uri
            };
        }
    }
}
