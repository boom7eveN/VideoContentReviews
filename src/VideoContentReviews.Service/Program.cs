using VideoContentReviews.Service.IoC;
using VideoContentReviews.Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

var settings = VideoContentReviewsSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

MapperConfigurator.ConfigureServices(builder.Services);
AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureServices(builder);
SwaggerConfigurator.ConfigureServices(builder.Services, settings);
ServicesConfigurator.ConfigureServices(builder.Services, settings);
builder.Services.AddControllers();

var app = builder.Build();

AuthorizationConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.MapControllers();
app.Run();