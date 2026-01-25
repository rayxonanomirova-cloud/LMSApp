
using LMSApp.Application.Interfaces;
using LMSApp.Domain.Entities.Auth;
using LMSApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly LMSAppContext _dbContext;
        public UserService(LMSAppContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
