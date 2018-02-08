using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ClientSideComponent : ClientSideObject
    {
        [JsonProperty("ComponentType")]
        public long ComponentType { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Manifest")]
        public string Manifest { get; set; }

        [JsonProperty("ManifestType")]
        public long ManifestType { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Status")]
        public long Status { get; set; }

        public string Alias
        {
            get
            {
                try
                {
                    var dynManifest = JsonConvert.DeserializeObject<dynamic>(Manifest);
                    return dynManifest.alias;
                } catch
                {
                    return null;
                }
            }
        }
    }
}
