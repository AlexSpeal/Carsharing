using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Service.loC;

public class DbContextConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        var connectionString = configuration.GetValue<string>("CarsharingContext");

        builder.Services.AddDbContextFactory<CarsharingDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CarsharingDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}