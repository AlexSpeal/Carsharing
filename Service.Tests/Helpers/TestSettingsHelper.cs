using Service.Settings;

namespace Service.Tests.Helpers;

public static class TestSettingsHelper
{
    public static CarsharingSettings GetSettings()
    {
        return CarsharingSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}