
using System.ComponentModel;

namespace LMSApp.Domain.Models.JWT
{
    public class JWTSetting
    {
        public JWT JWT { get; set; }
    }
    public class JWT
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public int ExpiresInHours { get; set; } = 12;
    }
}
