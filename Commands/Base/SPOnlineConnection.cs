using System;
using System.Security;

namespace SharePointPnP.PowerShell.Core.Base
{
    public class SPOnlineConnection
    {
        public const string AppId = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        public string AccessToken {get;set;}
        public string RefreshToken {get;set;}
        public DateTime ExpiresIn {get;set;}

        public string Url {get;set;}

        public static SPOnlineConnection CurrentConnection { get; set; }
    }
}
