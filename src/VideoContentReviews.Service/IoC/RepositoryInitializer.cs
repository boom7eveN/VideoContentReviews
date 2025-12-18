using Microsoft.AspNetCore.Identity;
using VideoContentReviews.BL.Auth;
using VideoContentReviews.BL.Auth.Entities;
using VideoContentReviews.DataAccess.Entities;
using VideoContentReviews.DataAccess.Entities.Primitives;
using VideoContentReviews.Service.Settings;

namespace VideoContentReviews.Service.IoC;

public class RepositoryInitializer(VideoContentReviewsSettings videoContentReviewsDbSettings)
{
    private readonly string _masterAdminEmail = videoContentReviewsDbSettings.MasterAdminEmail;
    private readonly string _masterAdminPassword = videoContentReviewsDbSettings.MasterAdminPassword;
    private readonly string _masterUserName = videoContentReviewsDbSettings.MasterUserName;

    private async Task CreateGlobalAdmin(IAuthProvider authorizationProvider)
    {
        await authorizationProvider.RegisterUserAsync(new RegisterUserModel
        {
            UserName = _masterUserName,
            Email = _masterAdminEmail,
            Password = _masterAdminPassword,
            Role = UserRole.Moderator
        });
    }

    public async Task InitializeRepository(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var userManager = (UserManager<UserEntity>)scope.ServiceProvider
            .GetRequiredService(typeof(UserManager<UserEntity>));
        var user = await userManager.FindByEmailAsync(_masterAdminEmail);
        if (user == null)
        {
            var authorizationProvider = (IAuthProvider)scope.ServiceProvider
                .GetRequiredService(typeof(IAuthProvider));
            await CreateGlobalAdmin(authorizationProvider);
        }
    }
}