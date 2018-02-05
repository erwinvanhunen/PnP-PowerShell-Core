using System.Collections.Generic;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ClientSideObject : IClientSideObject
    {
        private SPOnlineConnection _context;

        public ClientSideObject()
        {
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