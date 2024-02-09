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
        services.ConfigureApplicationSecurity(Configuration);
        // services.ConfigureHealthChecks(Configuration);
        // services.ConfigureProblemDetails();
        // services.ConfigureApiVersioning();
        services.AddInfrastructure(Configuration);
        services.AddSwaggerGen(c =>
                {
                    var securityScheme = new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Description = "Enter a Bearer Token into the `Value` field to have it automatically prefixed with `Bearer ` and used as an `Authorization` header value for requests.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    c.AddSecurityDefinition("Bearer", securityScheme);

                    c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            { securityScheme, Array.Empty<string>() }
                        });
                });
        // services.ConfigureSwagger(Configuration);
        services.AddDataProtection();
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
