using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using VideoContentReviews.Service.Tests.Helpers;

namespace VideoContentReviews.Service.Tests;

public class TestBase
{
    private readonly WebApplicationFactory<Program> _testServer;
    protected HttpClient TestHttpClient => _testServer.CreateClient();

    public TestBase()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(() => _testServer.CreateClient());
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

    public T? GetService<T>() where T : notnull => _testServer.Services.GetRequiredService<T>();

    [OneTimeTearDown]
    public void OneTimeTearDown() => _testServer.Dispose();
}