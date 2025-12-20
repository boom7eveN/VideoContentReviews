using Microsoft.Extensions.Configuration;

namespace VideoContentReviews.Service.Tests.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        var assemblyLocation = typeof(ConfigurationHelper).Assembly.Location;
        var testProjectDirectory = Path.GetDirectoryName(assemblyLocation)!;
        var serviceProjectPath = Path.GetFullPath(Path.Combine(
            testProjectDirectory,
            "..", "..", "..", "..",
            "VideoContentReviews.Service"));

        return new ConfigurationBuilder()
            .SetBasePath(serviceProjectPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}