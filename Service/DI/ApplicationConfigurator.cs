using Service.loC;
using Service.Settings;

namespace Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, CarsharingSettings settings)
    {
        AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureService(builder.Services);
        DbContextConfigurator.ConfigureServices(builder, settings);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);
        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        AuthorizationConfigurator.ConfigureApplication(app);
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        app.MapControllers();
    }
}