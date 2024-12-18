using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BL.Tests.Users;

public class RepositoryTestsBaseClass
{
    public RepositoryTestsBaseClass()
    {

        ServiceProvider = ConfigureServiceProvider();
        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<CarsharingDbContext>>();

  
        var dbContext = DbContextFactory.CreateDbContext();
        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<CarsharingDbContext>>(); 
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
      
        serviceCollection.AddDbContextFactory<CarsharingDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        
        return serviceCollection.BuildServiceProvider();
    }

    protected readonly IServiceProvider ServiceProvider;
    protected readonly IDbContextFactory<CarsharingDbContext> DbContextFactory;
}   