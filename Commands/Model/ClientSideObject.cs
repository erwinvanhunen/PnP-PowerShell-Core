using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public abstract class ClientSideObject : IClientSideObject
    {
        private SPOnlineConnection _context;

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

        public ClientSideObject(SPOnlineConnection context)
        {
            _context = context;
        }

        [JsonIgnore]
        public SPOnlineConnection Context
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
    }
}