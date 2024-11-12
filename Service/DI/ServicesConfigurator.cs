using AutoMapper;
using BL.Users.Manager;
using BL.Users.Provider;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Service.Settings;

namespace Service.DI;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<UserEntity>>(x => 
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<CarsharingDbContext>>()));
        services.AddScoped<IUserProvider>(x => 
            new UserProvider(x.GetRequiredService<IRepository<UserEntity>>(), 
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUserManager>(x =>
            new UserManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
    }
}