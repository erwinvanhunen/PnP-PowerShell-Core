using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Group : Principal
    {

        public Group() : base("SP.Group")
        { }

        [JsonProperty("AllowMembersEditMembership")]
        public bool AllowMembersEditMembership { get; set; }

        [JsonProperty("AllowRequestToJoinLeave")]
        public bool AllowRequestToJoinLeave { get; set; }

        [JsonProperty("AutoAcceptRequestToJoinLeave")]
        public bool AutoAcceptRequestToJoinLeave { get; set; }

        [JsonProperty("Description")]
        public object Description { get; set; }

        [JsonProperty("OnlyAllowMembersViewMembership")]
        public bool OnlyAllowMembersViewMembership { get; set; }

        [JsonProperty("OwnerTitle")]
        public string OwnerTitle { get; set; }

        [JsonProperty("RequestToJoinLeaveEmailSetting")]
        public string RequestToJoinLeaveEmailSetting { get; set; }
    }
}
