using Auth.Api;
using Auth.Api.Endpoints.Constants;
using Auth.Application;
using Auth.Infrastructure;

using Scalar.AspNetCore;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseStatusCodePages();
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent);
        };
    });

    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Auth Server API");
            options.WithTheme(ScalarTheme.BluePlanet);
            options.WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Curl);
        });
    }

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapAuthEndpoints();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Sever terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
