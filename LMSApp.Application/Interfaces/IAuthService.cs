
using LMSApp.Domain.Models.LoginModels;
using LMSApp.Domain.Models.TokenModels;

namespace LMSApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> Login(LoginModel loginModel);
    }
}
