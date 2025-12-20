using System.Net;
using System.Net.Http.Json;
using VideoContentReviews.BL.Features.Auth.DTOs;
using VideoContentReviews.Service.Controllers.Authorization.DTOs;
using VideoContentReviews.Service.Controllers.Users.DTOs.Responses;

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

    protected override async Task AdditionalOneTimeSetUp()
    {
        var request = new RegisterUserRequest
        {
            UserName = "testuser",
            Email = "test@test.test",
            Password = "P@ssw0rd"
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RegisterUser, request);
        var content = await response.Content.ReadFromJsonAsync<UserResponse>();
    }


    [Test]
    public async Task Login_Success()
    {
        var request = new AuthorizeUserRequest
        {
            Email = "test@test.test",
            Password = "P@ssw0rd"
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Login_InvalidCredentials_Failure()
    {
        var request = new AuthorizeUserRequest
        {
            Email = "test@test.test",
            Password = ValidPassword
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.LoginUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Login_UserNotFound_Failure()
    {
        var request = new AuthorizeUserRequest
        {
            Email = "neverusedmail@forregistration.esketit",
            Password = ValidPassword
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
        var loginRequest = new AuthorizeUserRequest
        {
            Email = "test@test.test",
            Password = "P@ssw0rd"
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
        var request = new RegisterUserRequest
        {
            UserName = "newuser",
            Email = "newuser@test.com",
            Password = "P@ssw0rd"
        };

        var response = await TestHttpClient.PostAsJsonAsync(
            AppEndpoints.Endpoints.RegisterUser, request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
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
}