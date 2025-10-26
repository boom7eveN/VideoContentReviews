using VideoContentReviews.Service.IoC;
using VideoContentReviews.Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

var settings = VideoContentReviewsSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureServices(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.MapGet("/", () => "ОНО РАБОТАЕТ ОНО РАБОТАЕТ");


app.Run();