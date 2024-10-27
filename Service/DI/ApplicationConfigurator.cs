using Service.loC;

namespace Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureService(builder.Services);
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
    }
}