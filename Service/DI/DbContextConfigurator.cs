using DataAccess;
using Microsoft.EntityFrameworkCore;
using Service.Settings;

namespace Service.loC;

public class DbContextConfigurator
{
    public static void ConfigureServices(IServiceCollection services, CarsharingSettings settings)
    {
        var connectionString = settings.ConnectionString;
        services.AddDbContextFactory<CarsharingDbContext>(
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