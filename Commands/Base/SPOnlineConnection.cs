using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;

namespace SharePointPnP.PowerShell.Core.Base
{
    public class SPOnlineConnection
    {
        private string _accessToken;
        private string _refreshToken;
        public const string AppId = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        private string _moduleBase;
        public SPOnlineConnection(string moduleBase)
        {
            _moduleBase = moduleBase;
        }

        //public string AccessToken {get;set;}
        public string RefreshToken
        {
            get
            {
                return _refreshToken;
            }
            set
            {
                _refreshToken = value;
            }
        }

        public DateTime ExpiresIn { get; set; }

        public string Url { get; set; }

        public static SPOnlineConnection CurrentConnection { get; set; }

        public string AccessToken
        {
            get
            {
                if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now > ExpiresIn && !string.IsNullOrEmpty(RefreshToken))
                {
                    // Expired token
                    var client = new HttpClient();
                    var uri = new Uri(Url);
                    var url = $"{uri.Scheme}://{uri.Host}";
                    var body = new StringContent($"resource={url}&client_id={SPOnlineConnection.AppId}&grant_type=refresh_token&refresh_token={_refreshToken}");
                    body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                    var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    _accessToken = tokens["access_token"];
                    _refreshToken = tokens["refresh_token"];
                    ExpiresIn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
                    var credmgr = new CredentialManager(_moduleBase);
                    credmgr.Add(Url, _accessToken, _refreshToken, ExpiresIn);
                }
                return _accessToken;
            }
            set
            {
                _accessToken = value;
            }
        }

        public SPOnlineConnection Clone(string url)
        {
            var connection = new SPOnlineConnection(_moduleBase);
            connection.Url = url;
            connection.AccessToken = AccessToken;
            connection.RefreshToken = RefreshToken;
            return connection;
        }
    }
}
