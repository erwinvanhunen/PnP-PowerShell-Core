using System.Net.Http;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public class ClientRequestHelper
    {
        public string Send(SPOnlineContext context, string content)
        {
            //<Request AddExpandoFieldTypeSuffix="true" SchemaVersion="15.0.0.0" LibraryVersion="16.0.0.0" ApplicationName="SharePoint PnP PowerShell Library" xmlns="http://schemas.microsoft.com/sharepoint/clientquery/2009"><Actions><ObjectPath Id="4" ObjectPathId="3" /><Query Id="5" ObjectPathId="3"><Query SelectAllProperties="false"><Properties><Property Name="CorporateCatalogUrl" ScalarProperty="true" /></Properties></Query></Query></Actions><ObjectPaths><StaticProperty Id="3" TypeId="{e9a11c41-0667-4c14-a4a5-e0d6cf67f6fa}" Name="Current" /></ObjectPaths></Request>
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