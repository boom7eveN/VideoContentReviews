using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public class AuthorizationConfigurator
{
     public static void ConfigureServices(IServiceCollection services, VideoContentReviewsSettings settings)
    {
        IdentityModelEventSource.ShowPII = true;
        services.AddIdentity<UserEntity, UserRoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<VideoContentReviewsDbContext>()
            .AddSignInManager<SignInManager<UserEntity>>()
            .AddDefaultTokenProviders()
            .AddRoles<UserRoleEntity>();
        
        services.AddIdentityServer()
            .AddInMemoryApiScopes([new ApiScope("api")])
            .AddInMemoryClients(
            [
                new Client
                {
                    ClientName = settings.ClientId,
                    ClientId = settings.ClientId,
                    Enabled = true,
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = [
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword
                    ],
                    ClientSecrets = [new Secret(settings.ClientSecret.Sha256())],
                    AllowedScopes = ["api"]
                },
                new Client
                {
                    ClientId = "swagger",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = [new Secret("swagger".Sha256())],
                    AllowedScopes = ["api"]
                },
            ])
            .AddAspNetIdentity<UserEntity>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.Authority = settings.IdentityServerUri;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            options.Audience = "api";
        });
        services.AddAuthorization();
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}