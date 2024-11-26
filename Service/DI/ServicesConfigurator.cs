using AutoMapper;
using BL.Authorization;
using BL.Users.Manager;
using BL.Users.Provider;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Settings;

namespace Service.DI;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, CarsharingSettings settings)
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

        services.AddScoped<IRepository<EmployeeEntity>>(x =>
            new Repository<EmployeeEntity>(x.GetRequiredService<IDbContextFactory<CarsharingDbContext>>()));

        services.AddScoped<IAuthProvider>(x =>
            new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                settings.IdentityServerUri!,
                settings.ClientId!,
                settings.ClientSecret!,
                x.GetRequiredService<IMapper>()
            ));
    }
}