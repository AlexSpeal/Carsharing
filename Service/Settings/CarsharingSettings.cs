namespace Service.Settings;

public class CarsharingSettings
{
    public string ConnectionString { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? IdentityServerUri { get; set; }
}