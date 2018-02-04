using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Helpers;

namespace SharePointPnP.PowerShell.Core.Base
{
    public class PnPCmdlet : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(SPOnlineConnection.AccessToken))
            {
                throw new PSInvalidOperationException("A connection is required. Use Connect-PnPOnline to connect first.");
            }
            //var expiresOn = SPOnlineConnection.ExpiresOn;
            // token expired?
            if (!string.IsNullOrEmpty(SPOnlineConnection.AccessToken) && DateTime.Now > SPOnlineConnection.ExpiresOn && !string.IsNullOrEmpty(SPOnlineConnection.RefreshToken))
            {
                // Expired token
                var client = new HttpClient();
                var uri = new Uri(SPOnlineConnection.Url);
                var url = $"{uri.Scheme}://{uri.Host}";
                var body = new StringContent($"resource={url}&client_id={SPOnlineConnection.AppId}&grant_type=refresh_token&refresh_token={SPOnlineConnection.RefreshToken}");
                body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                SPOnlineConnection.AccessToken = tokens["access_token"];
                SPOnlineConnection.RefreshToken = tokens["refresh_token"];
                SPOnlineConnection.ExpiresOn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
                var credmgr = new CredentialManager(MyInvocation.MyCommand.Module.ModuleBase);
                credmgr.Add(SPOnlineConnection.Url, SPOnlineConnection.AccessToken, SPOnlineConnection.RefreshToken, SPOnlineConnection.ExpiresOn);
            }
            ExecuteCmdlet();
        }

        protected virtual void ExecuteCmdlet()
        { }

        public T ExecuteGetRequest<T>(string method, string select = null, string filter = null, string expand = null)
        {
            return Helpers.RestHelper.ExecuteGetRequest<T>(method, select, filter, expand);
        }

        public string ExecuteGetRequest(string method, string select = null, string filter = null, string expand = null)
        {
            return Helpers.RestHelper.ExecuteGetRequest(method, select, filter, expand);
        }

    }
}