using System;

namespace SharePointPnP.PowerShell.Core.Base
{
    public static class SPOnlineConnection
    {
          public const string AppId = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        public static string AccessToken {get;set;}
        public static string RefreshToken {get;set;}
        public static DateTime ExpiresOn {get;set;}

        public static string Url {get;set;}
    }
}
