using System.Security.Cryptography;
using LMSApp.Application.Extensions;
using LMSApp.Application.Interfaces;
using LMSApp.Domain.Entities.Auth;
using LMSApp.Domain.Models.JWT;
using LMSApp.Domain.Models.LoginModels;
using LMSApp.Domain.Models.TokenModels;
using LMSApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LMSApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly JWTSetting jWTSetting;
        private readonly LMSAppContext _dbContext;

        public AuthService(UserManager<ApplicationUser> userManager, LMSAppContext dbContext, RoleManager<ApplicationRole> roleManager, IOptions<JWTSetting> options)
        {
            _dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jWTSetting = options.Value;

        }
        public async Task<TokenModel> LoginAsync(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
                return null;

            if (await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var token = await TokenExtension.GetTokenAsync(user, userManager, roleManager, jWTSetting);

                user.RefreshToken = await GetReffreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(jWTSetting.JWT.ExpiresRefreshTokenInHours);
                user.UpdateLastActive();

                await userManager.UpdateAsync(user);

                return new TokenModel
                {
                    AccessToken = token.AccessToken,
                    RefreshToken = user.RefreshToken
                };
            }

            return null;
        }

        public async Task<AccessTokenModel> AccessTokenAsync(string refreshToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            var token = await TokenExtension.GetTokenAsync(user, userManager, roleManager, jWTSetting);
            return new AccessTokenModel
            {
                AccessToken = token.AccessToken,
            };
        }

        public async Task<bool> GogoutAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            user.LastActive = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<string> GetReffreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
