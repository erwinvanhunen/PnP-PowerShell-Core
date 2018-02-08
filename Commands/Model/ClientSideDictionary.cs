using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ClientSideDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IClientSideObject
    {
        private SPOnlineContext _context;

        public ClientSideDictionary() : base() { }
        public ClientSideDictionary(int capacity) : base(capacity) { }

        public SPOnlineContext Context
        {
            get
            {
                return _context;
            }
            set { _context = value; }
        }
    }
}
