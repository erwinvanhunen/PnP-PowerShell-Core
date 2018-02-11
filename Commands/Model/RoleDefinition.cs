using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class RoleDefinition : ClientSideObject
    {
        public RoleDefinition() : base("SP.RoleDefinition")
        { }

        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }

        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("odata.id")]
        public string OdataId { get; set; }

        [JsonProperty("odata.editLink")]
        public string OdataEditLink { get; set; }

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
