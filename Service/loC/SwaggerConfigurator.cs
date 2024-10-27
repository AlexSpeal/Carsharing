namespace Service.loC;

public static class SwaggerConfigurator
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    public static void ConfigureApplication(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}