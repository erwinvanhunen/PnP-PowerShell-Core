using System.Collections.Generic;
using Newtonsoft.Json;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ClientSideObject
    {
        private string apiEndPoint;

        [JsonIgnore]
        protected internal Dictionary<string, object> ObjectProperties;

        [JsonIgnore]
        protected internal MetadataType metadataType { get; }

        public ClientSideObject()
        {
            this.ObjectProperties = new Dictionary<string, object>();
        }

        public ClientSideObject(string endPoint, string type)
        {
            this.apiEndPoint = endPoint;
            this.metadataType = new MetadataType(type);
            this.ObjectProperties = new Dictionary<string, object>();
        }
        public void Update()
        {
            var props = this.ObjectProperties;
            props["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(props);
            new RestRequest($"{apiEndPoint}").Merge(content, contentType: "application/json;odata=verbose");
        }
    }
}