namespace Service.Settings;

public static class CarsharingSettingsReader
{
    public static CarsharingSettings Read(IConfiguration configuration)
    {
        return new CarsharingSettings()
        {
            ServiceUri = configuration.GetValue<Uri>("Uri"),
            ConnectionString = configuration.GetValue<string>("CarsharingContext")
        };
    }
}