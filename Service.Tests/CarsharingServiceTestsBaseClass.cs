using System.Net.Http;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using NUnit.Framework;
using Service.Tests.Helpers;

namespace Service.Tests.Controllers;

public class CarsharingServiceTestsBaseClass
{
    public CarsharingServiceTestsBaseClass()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var CarRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<CarsharingDbContext>();
    }

    public T? GetService<T>()
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    private readonly TestWebApplicationFactory _testServer;
    protected long TestUserId;
    protected HttpClient TestHttpClient => _testServer.CreateClient();
}