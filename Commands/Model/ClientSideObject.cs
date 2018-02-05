using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public abstract class ClientSideObject : IClientSideObject
    {
        private MetadataType _metadataType;
        private SPOnlineConnection _context;

        [JsonProperty("__metadata")]
        public MetadataType MetadataType
        {
            get
            {
                return _metadataType;
            }
            set
            {
                _metadataType = value;
            }
        }
        public ClientSideObject()
        {
        }

        public ClientSideObject(string type)
        {
            _metadataType = new MetadataType(type);
        }

        public ClientSideObject(string endPoint, string type)
        {
        }

        public ClientSideObject(SPOnlineConnection context)
        {
            _context = context;
        }

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