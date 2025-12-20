using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VideoContentReviews.BL.Features.Auth;
using VideoContentReviews.BL.Features.Auth.DTOs;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.Service.Controllers.Authorization.DTOs;

namespace VideoContentReviews.Service.Tests.Controllers;

public class AuthControllerTests : TestBase
{
    private const string ValidUsername = "testusertest";
    private const string ValidEmail = "testing@test.test";
    private const string ValidPassword = "CorrectP@ssw0rd1";

    private const string EmptyString = "";
    private const string InvalidEmail = "invalidemail";
    private const string EmailWithoutAt = "emailwithoutat.com";
    private const string EmailWithoutDomain = "test@";
    private const string WeakPassword = "weak";

    [Test]
    public async Task Login_Success()
    {
        var email = "test@test.test";
        var password = "P@ssw0rd";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            await authProvider.RegisterUserAsync(new RegisterUserModel
            {
                UserName = "testuser",
                Email = email,
                Password = password
            });
        }

        var request = new AuthorizeUserRequest
        {
            Email = email,
            Password = password
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        await CleanupUser(userManager, email);
    }

    [Test]
    public async Task Login_InvalidCredentials_Failure()
    {
        var email = "test@test.test";
        var password = "P@ssw0rd";
        var wrongPassword = ValidPassword;

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            await authProvider.RegisterUserAsync(new RegisterUserModel
            {
                UserName = "testuser",
                Email = email,
                Password = password
            });
        }

        var request = new AuthorizeUserRequest
        {
            Email = email,
            Password = wrongPassword
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));

        await CleanupUser(userManager, email);
    }

    [Test]
    public async Task Login_UserNotFound_Failure()
    {
        var email = "neverusedmail@forregistratio.ru";
        var password = ValidPassword;

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            await userManager.DeleteAsync(user);
        }

        var request = new AuthorizeUserRequest
        {
            Email = email,
            Password = password
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    [TestCase(EmptyString, EmptyString)]
    [TestCase(ValidEmail, EmptyString)]
    [TestCase(InvalidEmail, EmptyString)]
    [TestCase(InvalidEmail, ValidPassword)]
    [TestCase(InvalidEmail, WeakPassword)]
    public async Task Login_Validation_Failure(string email, string password)
    {
        var request = new AuthorizeUserRequest
        {
            Email = email,
            Password = password
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Refresh_Success()
    {
        var email = "test@test.test";
        var password = "P@ssw0rd";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            await authProvider.RegisterUserAsync(new RegisterUserModel
            {
                UserName = "testuser",
                Email = email,
                Password = password
            });
        }

        var loginRequest = new AuthorizeUserRequest
        {
            Email = email,
            Password = password
        };
        var loginResponse = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, loginRequest);
        var content = await loginResponse.Content.ReadFromJsonAsync<TokensResponse>();
        var request = new RefreshTokenRequest
        {
            RefreshToken = content!.RefreshToken!
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RefreshToken, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        await CleanupUser(userManager, email);
    }

    [Test]
    public async Task Refresh_Failure()
    {
        var request = new RefreshTokenRequest
        {
            RefreshToken = "invalid"
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RefreshToken, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }

    [Test]
    public async Task Register_Success()
    {
        var email = "newuser@test.com";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        await CleanupUser(userManager, email);

        var request = new RegisterUserRequest
        {
            UserName = "newuser",
            Email = email,
            Password = "P@ssw0rd"
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RegisterUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        await CleanupUser(userManager, email);
    }

    [Test]
    [TestCase(ValidUsername, EmptyString, ValidPassword)]
    [TestCase(ValidUsername, InvalidEmail, ValidPassword)]
    [TestCase(ValidUsername, EmailWithoutAt, ValidPassword)]
    [TestCase(ValidUsername, EmailWithoutDomain, ValidPassword)]
    [TestCase(ValidUsername, ValidEmail, EmptyString)]
    [TestCase(EmptyString, EmptyString, EmptyString)]
    [TestCase(ValidUsername, InvalidEmail, WeakPassword)]
    [TestCase(EmptyString, ValidEmail, ValidPassword)]
    public async Task Register_Validation_Failure(string username, string email, string password)
    {
        var request = new RegisterUserRequest
        {
            UserName = username,
            Email = email,
            Password = password
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RegisterUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    private static async Task CleanupUser(UserManager<UserEntity> userManager, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            await userManager.DeleteAsync(user);
        }
    }
}