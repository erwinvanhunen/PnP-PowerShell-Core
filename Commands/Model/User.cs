using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class User : Principal
    {
        public User() : base("SP.User")
        { }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("IsEmailAuthenticationGuestUser")]
        public bool IsEmailAuthenticationGuestUser { get; set; }

        [JsonProperty("IsShareByEmailGuestUser")]
        public bool IsShareByEmailGuestUser { get; set; }

        [JsonProperty("IsSiteAdmin")]
        public bool IsSiteAdmin { get; set; }

        [JsonProperty("UserId")]
        public UserId UserId { get; set; }
    }

    public class UserId
    {
        [JsonProperty("NameId")]
        public string NameId { get; set; }

        [JsonProperty("NameIdIssuer")]
        public string NameIdIssuer { get; set; }
    }
}
