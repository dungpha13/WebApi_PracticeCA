using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
        // JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddTransient<ICurrentUserService, CurrentUserService>();
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

        services.AddCors(options =>
        {
            options.AddPolicy("AllOrigins",
            builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(
            cfg =>
            {
                cfg.Authority = configuration.GetSection("Security.Bearer:Authority").Get<string>();
                cfg.Audience = configuration.GetSection("Security.Bearer:Audience").Get<string>();

                cfg.TokenValidationParameters.RoleClaimType = "role";
                cfg.Configuration = new OpenIdConnectConfiguration();
                cfg.SaveToken = true;
                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context.SecurityToken is JwtSecurityToken accessToken && context.Principal.Identity is ClaimsIdentity identity)
                        {
                            identity.AddClaim(new Claim("access_token", accessToken.RawData));
                        }

                        return Task.CompletedTask;
                    }
                };
            })
        .AddCookie();

        services.AddAuthorization(ConfigureAuthorization);

        return services;
    }


    private static void ConfigureAuthorization(AuthorizationOptions options)
    {
        //Configure policies and other authorization options here. For example:
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "Employee"));
        //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "admin"));
    }
}