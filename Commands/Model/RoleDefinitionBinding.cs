using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class RoleDefinitionBinding : ClientSideObject
    {

        public RoleDefinitionBinding() : base("SP.RoleDefinitionBinding")
        { }

        [JsonProperty("BasePermissions")]
        public BasePermissions BasePermissions { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Order")]
        public long Order { get; set; }

        [JsonProperty("RoleTypeKind")]
        public long RoleTypeKind { get; set; }
    }
}
