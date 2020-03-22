using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Basic.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basic
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config => {
                    config.Cookie.Name = "my.cookie";
                    config.LoginPath = "/Home/Authenticate";
                });

            services.AddAuthorization(config => {
                // var defaultAuthBuilder = new AuthorizationPolicyBuilder(); 
                // var defaultAuthPolicy = defaultAuthBuilder
                //     .RequireAuthenticatedUser()
                //     .RequireClaim(ClaimTypes.DateOfBirth)
                //     .Build();
                // config.AddPolicy("Claim.DOB", policyBuilder => {
                //    policyBuilder.RequireClaim(ClaimTypes.DateOfBirth); 
                // });
                config.AddPolicy("Claim.DOB", policyBuilder => {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
