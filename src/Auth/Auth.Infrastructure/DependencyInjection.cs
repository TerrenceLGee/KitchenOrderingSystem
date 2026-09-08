using Auth.Application.Abstractions;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Services;
using Auth.Infrastructure.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName)
                              .Get<JwtSettings>()
                          ?? throw new InvalidOperationException(
                              $"Unable to retrieve {JwtSettings.SectionName} settings.");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (_, _, _, _) =>
                    {
                        var httpClient = new HttpClient();
                        var jwksUrl = $"{jwtSettings.Issuer}/.well-known/jwks.json";
                        var jwks = httpClient.GetStringAsync(jwksUrl)
                            .Result;
                        var keys = new JsonWebKeySet(jwks);
                        return keys.Keys;
                    }
                };
            });

        services.AddAuthorization();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}