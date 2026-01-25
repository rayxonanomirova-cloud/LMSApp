
using System.Security.Claims;
using LMSApp.Domain.Models;

namespace LMSApp.Application.Helpers
{
    public static class HelperUserProfile
    {
        public static UserProfile GetUserProfile(ClaimsPrincipal model)
        {
            var clims = model.Claims;

            return new UserProfile()
            {
                Id = Guid.Parse(clims.FirstOrDefault(x => x.Type == "id").Value),
                UserName = clims.FirstOrDefault(x => x.Type == "UserName").Value,
                ActiveRole = clims.FirstOrDefault(x => x.Type == "MainRoleId").Value,
                Roles = clims.Where(x => x.Type == "AllRoles").Select(x => x.Value).ToArray()
            };
        }
    }
}
