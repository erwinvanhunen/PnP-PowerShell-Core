namespace SharePointPnP.PowerShell.Core.Model
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class RoleAssignment : ClientSideObject
    {
        public RoleAssignment() : base("SP.RoleAssignment")
        { }

        [JsonProperty("PrincipalId")]
        public long PrincipalId { get; set; }
    }
}
