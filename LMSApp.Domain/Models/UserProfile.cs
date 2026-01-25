
namespace LMSApp.Domain.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ActiveRole { get; set; }
        public string[] Roles { get; set; }
    }
}
