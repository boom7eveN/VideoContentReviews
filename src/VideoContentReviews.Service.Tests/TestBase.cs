using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using Npgsql;
using Respawn;
using Respawn.Graph;
using VideoContentReviews.Service.Tests.Helpers;

namespace VideoContentReviews.Service.Tests;


public class TestBase
{
    protected readonly WebApplicationFactory<Program> TestServer;

    protected static Respawner Respawner;
    
    private HttpClient? _client;
    protected HttpClient TestHttpClient => _client ??= TestServer.CreateClient();

    public TestBase()
    {
        var settings = TestSettingsHelper.GetSettings();

        TestServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(() => TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                var httpClient = new HttpClient();
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(httpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }
    
    public T GetService<T>() where T : notnull  => TestServer.Services.GetRequiredService<T>();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var settings = TestSettingsHelper.GetSettings();
        await using var conn = new NpgsqlConnection(settings.VideoContentReviewsDbConnectionString);
        await conn.OpenAsync();

        Respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = [new Table("public","__EFMigrationsHistory")]
        });
        await AdditionalOneTimeSetUp();
    }
    
    protected virtual Task AdditionalOneTimeSetUp()
    {
        return Task.CompletedTask;
    }
    
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        var settings = TestSettingsHelper.GetSettings();
        await using var conn = new NpgsqlConnection(settings.VideoContentReviewsDbConnectionString);
        await conn.OpenAsync();
        await Respawner.ResetAsync(conn);
        await TestServer.DisposeAsync();
    }
    
}