using Service.loC;
using Service.Settings;

namespace Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, CarsharingSettings settings)
    {
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureService(builder.Services);
        DbContextConfigurator.ConfigureServices(builder.Services, settings);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services);
        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        app.MapControllers();
    }
}