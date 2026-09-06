using Auth.Api.Handlers;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

using Serilog;

namespace Auth.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
            };
        });

        services.AddSerilog((srvs, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(srvs));

        services.AddControllers();

        services.AddHostedService<KeyRotationService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                var info = document.Info;
                info.Title = "Auth Server";
                info.Description = "A Auth Server that authenticates an end user and dispenses access and refresh tokens for authorization";
                info.Contact = new OpenApiContact
                {
                    Name = "Terrence L. Gee",
                    Email = "mrgee1978@proton.me",
                    Url = new Uri("https://github.com/TerrenceLGee?tab=repositories")
                };
                document.Info = info;

                var components = document.Components ?? new OpenApiComponents();
                components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Enter your JWT token"
                };

                document.Components = components;

                var schemeReference = new OpenApiSecuritySchemeReference("Bearer");
                var securityRequirement = new OpenApiSecurityRequirement { [schemeReference] = [] };

                document.Security ??= [];
                document.Security.Add(securityRequirement);
                return Task.CompletedTask;
            });
        });

        return services;
    }
}