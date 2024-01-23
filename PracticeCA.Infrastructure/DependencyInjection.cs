using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeCA.Domain;

namespace PracticeCA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            // options.UseLazyLoadingProxies();
        });
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IUserRepository, UserRepository>();
        // services.AddIdentity<User, Role>(options =>
        // {
        //     // Configure Identity options here
        //     options.Password.RequireDigit = false;
        //     options.Password.RequiredLength = 6;
        //     options.Password.RequireLowercase = false;
        //     options.Password.RequireUppercase = false;
        //     options.Password.RequireNonAlphanumeric = false;

        //     // Lockout settings
        //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        //     options.Lockout.MaxFailedAccessAttempts = 5;

        //     // User settings
        //     options.User.RequireUniqueEmail = false;
        // })
        //         .AddEntityFrameworkStores<ApplicationDbContext>()
        //         .AddDefaultTokenProviders();
        return services;
    }
}
