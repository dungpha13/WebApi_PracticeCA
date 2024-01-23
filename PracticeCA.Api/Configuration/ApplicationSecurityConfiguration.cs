using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PracticeCA.Application;
using PracticeCA.Domain;
using PracticeCA.Infrastructure;

namespace PracticeCA.Api.Configuration;

public static class ApplicationSecurityConfiguration
{
    public static IServiceCollection ConfigureApplicationSecurity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        services.AddHttpContextAccessor();

        services.AddIdentity<User, Role>(options =>
        {
            // Configure Identity options here
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            // options.Lockout.MaxFailedAccessAttempts = 5;

            // User settings
            options.User.RequireUniqueEmail = false;
        })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority = configuration.GetSection("Security.Bearer:Authority").Get<string>();
                    options.Audience = configuration.GetSection("Security.Bearer:Audience").Get<string>();

                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.SaveToken = true;
                });

        services.AddAuthorization(ConfigureAuthorization);

        return services;
    }


    private static void ConfigureAuthorization(AuthorizationOptions options)
    {
        //Configure policies and other authorization options here. For example:
        //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "employee"));
        //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "admin"));
    }
}