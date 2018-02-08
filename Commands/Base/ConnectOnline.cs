using System.Management.Automation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Base
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Connect, "Online")]
    [CmdletHelp(VerbsCommunications.Connect, "Online", "Connects to your tenant")]
    public class ConnectOnline : PSCmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Url")]
        public string Url;

        [Parameter(Mandatory = false)]
        public string AppId;

        [Parameter(Mandatory = false)]
        public SwitchParameter ReturnContext; 

        protected override void ProcessRecord()
        {
            var credManager = new CredentialManager(MyInvocation.MyCommand.Module.ModuleBase);
            var credManagerContext = credManager.Get(Url);
            if(credManagerContext != null)
            {
                SPOnlineContext.CurrentContext = credManagerContext;
            } else { 
                var connectionUri = new Uri(Url);

                HttpClient client = new HttpClient();
                var result = client.GetStringAsync($"https://login.microsoftonline.com/common/oauth2/devicecode?resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={SPOnlineContext.AppId}").GetAwaiter().GetResult();
                var returnData = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                WriteObject(returnData["message"]);

                OpenBrowser(returnData["verification_url"]);

                //waiting for token

                var body = new StringContent($"resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={SPOnlineContext.AppId}&grant_type=device_code&code={returnData["device_code"]}");
                body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

                var tokenResult = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                while (!tokenResult.IsSuccessStatusCode)
                {
                    System.Threading.Thread.Sleep(1000);
                    tokenResult = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                }
                var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResult.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                var connection = new SPOnlineContext(MyInvocation.MyCommand.Module.ModuleBase);
                connection.AccessToken = tokens["access_token"];
                connection.RefreshToken = tokens["refresh_token"];
                connection.ExpiresIn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
                connection.Url = Url;
                credManager.Add(Url, connection.AccessToken, connection.RefreshToken, connection.ExpiresIn);
                SPOnlineContext.CurrentContext = connection;

            }
            if(ReturnContext)
            {
                WriteObject(SPOnlineContext.CurrentContext);
            }
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}