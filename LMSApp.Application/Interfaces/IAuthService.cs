
using LMSApp.Domain.Models.LoginModels;
using LMSApp.Domain.Models.TokenModels;

namespace LMSApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> LoginAsync(LoginModel loginModel);
        Task<AccessTokenModel> AccessTokenAsync(string refreshToken);
        Task<bool> GogoutAsync(Guid userId);
    }
}
