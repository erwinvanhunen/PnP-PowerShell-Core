using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class UpdatableClientSideObject : ClientSideObject
    {
        [JsonProperty("odata.editLink")]
        private string _editLink { get; set; }


        [JsonIgnore]
        protected internal Dictionary<string, object> ObjectProperties;

        [JsonIgnore]
        protected internal MetadataType metadataType { get; }

        public UpdatableClientSideObject()
        {
            this.ObjectProperties = new Dictionary<string, object>();
        }

        public UpdatableClientSideObject(string type)
        {
            this.metadataType = new MetadataType(type);
            this.ObjectProperties = new Dictionary<string, object>();
        }

        public UpdatableClientSideObject(SPOnlineConnection context) : base(context)
        {
        }

        public void Update()
        {
            var props = this.ObjectProperties;
            props["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(props);
            new RestRequest(Context, _editLink).Merge(content, contentType: "application/json;odata=verbose");
        }
    }
}