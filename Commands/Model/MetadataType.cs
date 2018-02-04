using Newtonsoft.Json;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class MetadataType
    {
        [JsonProperty("type")]
        public string _typename;

        public MetadataType(string typename)
        {
            this._typename = typename;
        }
    }
}