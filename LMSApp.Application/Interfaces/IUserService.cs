using LMSApp.Domain.Entities.Auth;

namespace LMSApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();

    }
}
