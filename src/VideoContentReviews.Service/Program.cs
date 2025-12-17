using VideoContentReviews.Service.IoC;
using VideoContentReviews.Service.Middleware;
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
SwaggerConfigurator.ConfigureServices(builder.Services);
ServicesConfigurator.ConfigureServices(builder.Services, settings);
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
AuthorizationConfigurator.ConfigureApplication(app); 
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
var repositoryInitializer = new RepositoryInitializer(settings);
await repositoryInitializer.InitializeRepository(app);
app.MapControllers();
app.Run();