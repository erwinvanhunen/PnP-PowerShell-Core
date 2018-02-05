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
        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used by cmdlet. Retrieve the value for this parameter by eiter specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.")] // do not remove '#!#99'
        [PnPParameter(Order = 99)]
        public SPOnlineConnection Connection = null;

        public SPOnlineConnection Context
        {
            get
            {
                return Connection ?? SPOnlineConnection.CurrentConnection;
            }
        }

        protected override void ProcessRecord()
        {
            var cmdletConnection = Connection ?? SPOnlineConnection.CurrentConnection;
            if (cmdletConnection.AccessToken.Length == 0)
            {
                throw new PSInvalidOperationException("A connection is required. Use Connect-PnPOnline to connect first.");
            }
            
            //var expiresOn = SPOnlineConnection.ExpiresOn;
            // token expired?
            //if (!string.IsNullOrEmpty(cmdletConnection.AccessToken) && DateTime.Now > cmdletConnection.ExpiresIn && !string.IsNullOrEmpty(cmdletConnection.RefreshToken))
            //{
            //    // Expired token
            //    var client = new HttpClient();
            //    var uri = new Uri(cmdletConnection.Url);
            //    var url = $"{uri.Scheme}://{uri.Host}";
            //    var body = new StringContent($"resource={url}&client_id={SPOnlineConnection.AppId}&grant_type=refresh_token&refresh_token={cmdletConnection.RefreshToken}");
            //    body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    var result = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
            //    var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            //    cmdletConnection.AccessToken = tokens["access_token"];
            //    cmdletConnection.RefreshToken = tokens["refresh_token"];
            //    cmdletConnection.ExpiresIn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
            //    var credmgr = new CredentialManager(MyInvocation.MyCommand.Module.ModuleBase);
            //    credmgr.Add(cmdletConnection.Url, cmdletConnection.AccessToken, cmdletConnection.RefreshToken, cmdletConnection.ExpiresIn);
            //}
            ExecuteCmdlet();
        }

        protected virtual void ExecuteCmdlet()
        { }

        public T ExecuteGetRequest<T>(SPOnlineConnection context, string method, string select = null, string filter = null, string expand = null) where T : ClientSideObject
        {
            return Helpers.RestHelper.ExecuteGetRequest<T>(context, method, select, filter, expand);
        }

        public string ExecuteGetRequest(SPOnlineConnection context, string method, string select = null, string filter = null, string expand = null)
        {
            return Helpers.RestHelper.ExecuteGetRequest(context, method, select, filter, expand);
        }

    }
}