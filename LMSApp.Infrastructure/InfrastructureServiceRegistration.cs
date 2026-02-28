using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMSApp.Domain.Entities.Auth;
using LMSApp.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMSApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context.LMSAppContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("LMSAppConnectionString"),
                builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
            },
            ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
            {
                option.Password.RequiredLength = 1;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;
            })
               .AddEntityFrameworkStores<LMSAppContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });
            return services;
        }

        public static WebApplication MainConfiguration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LMSAppContext>();
                //context.Database.Migrate();
            }
            return app;
        }
    }
}
