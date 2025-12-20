using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VideoContentReviews.Service.Tests;

public class TestWebApplicationFactory(Action<IServiceCollection>? configureServices = null)
    : WebApplicationFactory<Program>
{
    private static string GetServiceProjectPath()
    {
        var assemblyLocation = typeof(TestWebApplicationFactory).Assembly.Location;
        var testProjectDirectory = Path.GetDirectoryName(assemblyLocation)!;
        var serviceProjectPath = Path.GetFullPath(Path.Combine(
            testProjectDirectory,
            "..", "..", "..", "..",
            "VideoContentReviews.Service"));
        return serviceProjectPath;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var serviceProjectPath = GetServiceProjectPath();
        var originalDirectory = Directory.GetCurrentDirectory();

        try
        {
            Directory.SetCurrentDirectory(serviceProjectPath);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            return base.CreateHost(builder);
        }
        finally
        {
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var serviceProjectPath = GetServiceProjectPath();

        builder.UseContentRoot(serviceProjectPath);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(serviceProjectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        });

        builder.ConfigureServices(services => configureServices?.Invoke(services));

        builder.UseEnvironment("Development");
    }
}