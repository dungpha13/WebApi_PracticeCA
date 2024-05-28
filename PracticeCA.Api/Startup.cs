using PracticeCA.Infrastructure;
using PracticeCA.Application;
using PracticeCA.Api.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PracticeCA.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(
            opt =>
            {
                opt.Filters.Add<ExceptionFilter>();
            });
        services.AddApplication(Configuration);
        services.AddInfrastructure(Configuration);
        services.AddDataProtection();
        services.ConfigureApplicationSecurity(Configuration);
        services.ConfigureSwagger(Configuration);
        // services.ConfigureHealthChecks(Configuration);
        // services.ConfigureProblemDetails();
        // services.ConfigureApiVersioning();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // app.UseSerilogRequestLogging();
        // app.UseExceptionHandler();
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapDefaultHealthChecks();
            endpoints.MapControllers();
        });
        // app.UseSwashbuckle(Configuration);
    }
}
