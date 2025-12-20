using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace VideoContentReviews.Service.Tests;

public class TestBase
{
    private WebApplicationFactory<Program>? _testServer;
    private HttpClient? _httpClient;

    protected HttpClient TestHttpClient => _httpClient ??= TestServer.CreateClient();
    protected WebApplicationFactory<Program> TestServer => _testServer ??= CreateTestServer();

    private WebApplicationFactory<Program> CreateTestServer()
    {
        WebApplicationFactory<Program>? factory = null;

        factory = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(() => factory!.CreateClient());
                return httpClientFactoryMock.Object;
            }));

            services.PostConfigure<JwtBearerOptions>(
                Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority = null;
                    options.MetadataAddress = null;
                    options.ConfigurationManager = null;
                    options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        SignatureValidator = (token, _) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token),
                        NameClaimType = System.Security.Claims.ClaimTypes.Name,
                        RoleClaimType = System.Security.Claims.ClaimTypes.Role
                    };
                    options.RequireHttpsMetadata = false;
                });
        });

        return factory;
    }

    public T? GetService<T>() where T : notnull => TestServer.Services.GetRequiredService<T>();

    [SetUp]
    public virtual void SetUp()
    {
        _httpClient = null;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => _testServer?.Dispose();
}