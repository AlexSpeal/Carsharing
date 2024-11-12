using BL.Users.Mapper;
using Service.Controllers.Mapper;

namespace Service.DI;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UserBLProfile>();
            config.AddProfile<UsersServiceProfile>();
        });
    }
}