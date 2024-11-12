using Service.DI;
using Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var settings = CarsharingSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

ApplicationConfigurator.ConfigureServices(builder, settings);

var app = builder.Build();

ApplicationConfigurator.ConfigureApplication(app);

app.Run();