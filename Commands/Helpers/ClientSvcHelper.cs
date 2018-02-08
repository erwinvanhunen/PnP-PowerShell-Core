using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public static class ClientSvcHelper
    {
        public static string Execute(SPOnlineContext context, string payload, bool isAdmin = false)
        {
            var response = ExecuteInternalAsync(context, payload, isAdmin).GetAwaiter().GetResult();
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static Task<HttpResponseMessage> ExecuteInternalAsync(SPOnlineContext context, string payload, bool isAdmin)
        {
            var hostUrl = context.Url;
            if(isAdmin && !hostUrl.Contains("-admin.sharepoint."))
            {
                var uri = new Uri(hostUrl.ToLower().Replace(".sharepoint.","-admin.sharepoint."));
                hostUrl = $"{uri.Scheme}://{uri.Host}";
            }
            var content = new StringContent(payload);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", RestHelper.GetRequestDigest(context,isAdmin ? hostUrl : null).GetAwaiter().GetResult());
            var url = $"{hostUrl}/_vti_bin/client.svc/ProcessQuery";
            var returnValue = client.PostAsync(url, content);
            return returnValue;
        }
    }
}