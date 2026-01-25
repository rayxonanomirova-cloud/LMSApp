using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMSApp.Domain.Models.TokenModels
{
    public class TokenModel
    {
        public string Token { get; set; }

        [JsonPropertyName("exp")]
        public DateTime Expiration { get; set; }
    }
}
