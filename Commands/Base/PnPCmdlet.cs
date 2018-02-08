using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Security;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Base
{
    public class PnPCmdlet : PSCmdlet
    {
        private SPOnlineContext _newContext;

        [Parameter(Mandatory = false, Position = 99, HelpMessage = "Optional connection to be used by cmdlet. Retrieve the value for this parameter by eiter specifying -ReturnContext on Connect-PnPOnline or by executing Get-PnPContext.")] // do not remove '#!#99'
        [PnPParameter(Order = 99)]
        public SPOnlineContext Context
        {
            get
            {
                return _newContext ?? SPOnlineContext.CurrentContext;
            }
            set
            {
                _newContext = value;
            }
        }

        protected override void ProcessRecord()
        {
            var cmdletConnection = Context ?? SPOnlineContext.CurrentContext;
            if (cmdletConnection == null || string.IsNullOrEmpty(cmdletConnection.AccessToken))
            {
                throw new PSInvalidOperationException("A connection is required. Use Connect-PnPOnline to connect first.");
            }

            ExecuteCmdlet();
        }

        protected virtual void ExecuteCmdlet()
        { }

        public T ExecuteGetRequest<T>(SPOnlineContext context, string method, string select = null, string filter = null, string expand = null) where T : ClientSideObject
        {
            return Helpers.RestHelper.ExecuteGetRequest<T>(context, method, select, filter, expand);
        }

        public string ExecuteGetRequest(SPOnlineContext context, string method, string select = null, string filter = null, string expand = null)
        {
            return Helpers.RestHelper.ExecuteGetRequest(context, method, select, filter, expand);
        }

    }
}