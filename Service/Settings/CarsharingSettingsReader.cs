namespace Service.Settings;

public static class CarsharingSettingsReader
{
    public static CarsharingSettings Read(IConfiguration configuration)
    {
        return new CarsharingSettings
        {
            ConnectionString = configuration.GetValue<string>("CarsharingContext"),
            ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri")
        };
    }
}