using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace PracticeCA.Api;

public static class SwashbuckleConfiguration
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(
            options =>
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

                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                            { securityScheme, Array.Empty<string>() }
                    });

                // options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
                // options.SupportNonNullableReferenceTypes();
                // options.CustomSchemaIds(x => x.FullName);

                // var apiXmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                // if (File.Exists(apiXmlFile))
                // {
                //     options.IncludeXmlComments(apiXmlFile);
                // }

                // var applicationXmlFile = Path.Combine(AppContext.BaseDirectory, $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml");
                // if (File.Exists(applicationXmlFile))
                // {
                //     options.IncludeXmlComments(applicationXmlFile);
                // }
                // options.OperationFilter<AuthorizeCheckOperationFilter>();

            });

        // services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionSwaggerGenOptions>();

        return services;
    }
}
