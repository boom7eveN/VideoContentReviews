using AutoMapper;
using Duende.IdentityModel.Client;
using Duende.IdentityServer.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.BL.Auth.Validator.Users;
using VideoContentReviews.BL.User.Entities;
using VideoContentReviews.BL.User.Exception;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.BL.Auth;

public class AuthProvider(
    SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    IMapper mapper,
    string identityServerUri,
    string clientId,
    string clientSecret)
    : IAuthProvider
{
    public async Task<TokensResponse> AuthorizeUserAsync(AuthorizeUserModel model)
    {
        // Валидация входных данных
        var validator = new AuthorizeUserRequestValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(ResultCode.ValidationError, 
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }
        
        var verificationResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!verificationResult.Succeeded)
        {
            throw new BusinessLogicException(ResultCode.EmailOrPasswordIsIncorrect);
        }
        
        var client = httpClientFactory.CreateClient();
        var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (discoveryDocument.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = clientId,
            ClientSecret = clientSecret,
            UserName = user.UserName,
            Password = model.Password,
            Scope = "api offline_access"
        });

        if (tokenResponse.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }
        
        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }

    public async Task<UserModel> RegisterUserAsync(RegisterUserModel model)
    {
        var validator = new RegisterUserRequestValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessLogicException(ResultCode.ValidationError, 
                string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is not null)
        {
            throw new BusinessLogicException(ResultCode.UserAlreadyExists);
        }
        
        user = mapper.Map<UserEntity>(model);
        user.ExternalId = Guid.NewGuid();
        var time = DateTime.UtcNow;
        user.CreationTime = time;
        user.ModificationTime = time;
        user.UserName = model.Email;
        user.Role = model.Role;
        
        var createResult = await userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            throw new BusinessLogicException(ResultCode.UserCreationFailure,
                string.Join(Environment.NewLine, createResult.Errors.Select(e => e.Description)));
        }
        
        return mapper.Map<UserModel>(user);
    }

    public async Task<TokensResponse> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new BusinessLogicException(ResultCode.ValidationError, "Refresh token is required");
        }

        var client = httpClientFactory.CreateClient();
        var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (discoveryDocument.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }

        var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = clientId,
            ClientSecret = clientSecret,
            RefreshToken = refreshToken
        });
        
        if (tokenResponse.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }

        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }
}