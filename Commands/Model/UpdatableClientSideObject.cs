using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class UpdatableClientSideObject : ClientSideObject
    {
     


        [JsonIgnore]
        protected internal ConcurrentDictionary<string, object> ObjectProperties;

        [JsonIgnore]
        protected internal MetadataType metadataType { get; }

        public UpdatableClientSideObject()
        {
            this.ObjectProperties = new ConcurrentDictionary<string, object>();
        }

        public UpdatableClientSideObject(string type)
        {
            this.metadataType = new MetadataType(type);
            this.ObjectProperties = new ConcurrentDictionary<string, object>();
        }

        public UpdatableClientSideObject(SPOnlineContext context) : base(context)
        {
        }

        public void Update()
        {
            var props = this.ObjectProperties;
            props["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(props);
            new RestRequest(Context, ObjectPath).Merge(content, contentType: "application/json;odata=verbose");
            props.Clear();
        }
    }
}