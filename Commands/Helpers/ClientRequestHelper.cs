using SharePointPnP.PowerShell.Core.Model;
using System.Net.Http;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public class ClientRequestHelper
    {
        public string Send(SPOnlineContext context, string content)
        {
            var url = $"{context.Url}/_vti_bin/client.svc/ProcessQuery";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", RestHelper.GetRequestDigest(context).GetAwaiter().GetResult());

            var stringContent = new StringContent(content);
            var returnValue = client.PostAsync(url, stringContent).GetAwaiter().GetResult();
            return returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}