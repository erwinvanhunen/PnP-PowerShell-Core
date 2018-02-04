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
    [CmdletHelp(VerbsCommunications.Connect,"Online","Connects to your tenant")]
    public class ConnectOnline : PSCmdlet
    {

        /// <summary>
        ///     The name of the person to greet.
        /// </summary>
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Url")]
        public string Url { get; set; }

        [Parameter(Mandatory = false)]
        public string AppId { get; set; }

        /// <summary>
        ///  Perform Cmdlet processing.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(SPOnlineConnection.AccessToken))
            {
                var credManager = new CredentialManager(MyInvocation.MyCommand.Module.ModuleBase);
                if (!credManager.Get(Url))
                {
                    var connectionUri = new Uri(Url);

                    HttpClient client = new HttpClient();
                    var result = client.GetStringAsync($"https://login.microsoftonline.com/common/oauth2/devicecode?resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={SPOnlineConnection.AppId}").GetAwaiter().GetResult();
                    var returnData = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    WriteObject(returnData["message"]);

                    OpenBrowser(returnData["verification_url"]);

                    //waiting for token

                    var body = new StringContent($"resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={SPOnlineConnection.AppId}&grant_type=device_code&code={returnData["device_code"]}");
                    body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

                    var tokenResult = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                    while (!tokenResult.IsSuccessStatusCode)
                    {
                        System.Threading.Thread.Sleep(1000);
                        tokenResult = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                    }
                    var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResult.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    SPOnlineConnection.AccessToken = tokens["access_token"];
                    SPOnlineConnection.RefreshToken = tokens["refresh_token"];
                    SPOnlineConnection.ExpiresOn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
                    SPOnlineConnection.Url = Url;
                    credManager.Add(Url, SPOnlineConnection.AccessToken, SPOnlineConnection.RefreshToken, SPOnlineConnection.ExpiresOn);
                }
            }
            else
            {
                SPOnlineConnection.Url = Url;
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