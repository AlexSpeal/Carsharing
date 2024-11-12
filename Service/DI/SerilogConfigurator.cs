using Serilog;

namespace Service.loC;

public static class SerilogConfigurator
{
    public static void ConfigureService(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.WithCorrelationId()
                .ReadFrom.Configuration(context.Configuration);
        });
        builder.Services.AddHttpContextAccessor();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}