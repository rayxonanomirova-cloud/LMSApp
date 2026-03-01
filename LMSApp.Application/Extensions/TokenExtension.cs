using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LMSApp.Domain.Entities.Auth;
using LMSApp.Domain.Models.JWT;
using LMSApp.Domain.Models.TokenModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LMSApp.Application.Extensions
{
    public static class TokenExtension
    {
        private static List<Claim> authClaims { get; set; }

        private static void AddClaim(ApplicationUser user)
        {
            authClaims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.UserName ?? ""),
                 new Claim("Id", user.Id.ToString()),
                 new Claim("ActiveRole",user.MainRoleId.ToString() ?? ""),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
        }

        public static async Task<AccessTokenModel> GetTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,JWTSetting jWTSetting)
        {
            AddClaim(user);
            await AddRolesToClaims(userManager, roleManager, user);
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSetting.JWT.Secret));

            var token =new JwtSecurityToken(
                issuer: jWTSetting.JWT.ValidIssuer,
                audience: jWTSetting.JWT.ValidAudience,
                expires: DateTime.UtcNow.AddMinutes(jWTSetting.JWT.ExpdsiresAccessTokenInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new AccessTokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private static async Task AddRolesToClaims(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager, ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim("AllRoles", userRole));
            }

            if(user.MainRoleId == null && userRoles.Count > 0)
            {
                var mainRoleStr = userRoles.FirstOrDefault();

                var mainRole = await roleManager.FindByNameAsync(mainRoleStr);

                user.MainRoleId = mainRole?.Id;

                if (mainRole != null)
                    authClaims.Add(new Claim(ClaimTypes.Role, mainRoleStr));
            }
            else
            {
                var mainRole = await roleManager.FindByIdAsync(user.MainRoleId.ToString());
                if (mainRole != null)
                    authClaims.Add(new Claim(ClaimTypes.Role, mainRole?.Name));
            }
        }
    }
}
