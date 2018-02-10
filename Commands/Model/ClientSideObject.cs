using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public abstract class ClientSideObject : IClientSideObject
    {
        private SPOnlineContext _context;

        [JsonProperty("odata.editLink")]
        private string _editLink { get; set; }

        [JsonProperty("odata.type")]
        private string _objectType { set; get; }

        [JsonProperty("__metadata", Order = 0)]
        internal MetadataType MetadataType
        {
            get
            {
                return new MetadataType(_objectType);
            }
        }

        public ClientSideObject()
        {
        }

        public ClientSideObject(string type)
        {
            _objectType = type;
        }

        public ClientSideObject(SPOnlineContext context)
        {
            _context = context;
        }

        [JsonIgnore]
        public string ObjectPath
        {
            get
            {
                return _editLink;
            }
        }

        [JsonIgnore]
        public SPOnlineContext Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public bool ShouldSerialize_editLink() => false;

        public bool ShouldSerialize_objectType() => false;
    }
}