using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class SPOnlineContext
    {
        private Model.Web _web;
        private string _accessToken;
        private string _refreshToken;
        public const string AppId = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        private string _moduleBase;
        public SPOnlineContext(string moduleBase)
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

        public DateTime ExpiresOn { get; set; }

        public string Url { get; set; }

        public static SPOnlineContext CurrentContext { get; set; }

        public string AccessToken
        {
            get
            {
                if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now > ExpiresOn && !string.IsNullOrEmpty(RefreshToken))
                {
                    // Expired token
                    var client = new HttpClient();
                    var uri = new Uri(Url);
                    var url = $"{uri.Scheme}://{uri.Host}";
                    var body = new StringContent($"resource={url}&client_id={SPOnlineContext.AppId}&grant_type=refresh_token&refresh_token={_refreshToken}");
                    body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
                    var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    _accessToken = tokens["access_token"];
                    _refreshToken = tokens["refresh_token"];
                    ExpiresOn = DateTime.Now.AddSeconds(int.Parse(tokens["expires_in"]));
                    var credmgr = new CredentialManager(_moduleBase);
                    credmgr.Add(Url, _accessToken, _refreshToken, ExpiresOn);
                }
                return _accessToken;
            }
            set
            {
                _accessToken = value;
            }
        }

        public SPOnlineContext Clone(string url)
        {
            var connection = new SPOnlineContext(_moduleBase);
            connection.Url = url;
            connection.AccessToken = AccessToken;
            connection.RefreshToken = RefreshToken;
            return connection;
        }

        public Model.Web Web
        {
            get
            {
                if(_web == null)
                { 
                    _web = new RestRequest(this, "Web").Get<Model.Web>();
                }
                return _web;
            }
        }
    }
}
