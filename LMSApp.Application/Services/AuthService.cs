using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMSApp.Application.Extensions;
using LMSApp.Application.Interfaces;
using LMSApp.Domain.Entities.Auth;
using LMSApp.Domain.Models.JWT;
using LMSApp.Domain.Models.LoginModels;
using LMSApp.Domain.Models.TokenModels;
using LMSApp.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LMSApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly JWTSetting jWTSetting;
        private readonly LMSAppContext _dbContext;

        public AuthService(UserManager<ApplicationUser> userManager,LMSAppContext dbContext,RoleManager<ApplicationRole> roleManager,IOptions<JWTSetting> options)
        {
            _dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jWTSetting = options.Value;

        }
        public async Task<TokenModel> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
                return null;

            if(await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var token = await TokenExtension.GetTokenAsync(user, userManager, roleManager, jWTSetting);

                return token;
            }

            return null;
        }
    }
}
