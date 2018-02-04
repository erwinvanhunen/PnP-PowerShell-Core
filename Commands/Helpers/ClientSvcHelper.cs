using System.Net.Http;
using System.Xml.Linq;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public static class ClientSvcHelper
    {
        public static void Execute(string payload)
        {
            // XElement element = new XElement("Request");
            // element.Add(new XAttribute("AddExpandoFieldTypeSuffix","true"));
            // element.Add(new XAttribute("SchemaVersion","15.0.0.0"));
            // element.Add(new XAttribute("LibraryVersion","16.0.0.0"));
            // element.Add(new XAttribute("ApplicationName","SharePoint PnP PowerShell Core"));
            // element.Add(new XAttribute("xmlns","http://schemas.microsoft.com/sharepoint/clientquery/2009"));



            var content = new StringContent(payload);
            //<Request AddExpandoFieldTypeSuffix="true" SchemaVersion="15.0.0.0" LibraryVersion="16.0.0.0" ApplicationName="SharePoint PnP PowerShell Library" xmlns="http://schemas.microsoft.com/sharepoint/clientquery/2009"><Actions><Method Name="SetFieldValue" Id="14" ObjectPathId="9"><Parameters><Parameter Type="String">Demo2</Parameter><Parameter Type="String">Demo2</Parameter></Parameters></Method><Method Name="Update" Id="15" ObjectPathId="5" /></Actions><ObjectPaths><Property Id="9" ParentId="5" Name="AllProperties" /><Identity Id="5" Name="e82e479e-4047-5000-d2b3-60e4a5a07d64|740c6a0b-85e2-48a0-a494-e0f1759d4aa7:site:78a6e667-1286-4568-b03b-a169749835be:web:fd3a592d-ea6b-4201-814b-2b77d72728e6" /></ObjectPaths></Request>
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", RestHelper.GetRequestDigest().GetAwaiter().GetResult());


            var url = $"{SPOnlineConnection.Url}/_vti_bin/client.svc/ProcessQuery";
            var returnValue = client.PostAsync(url, content).GetAwaiter().GetResult();
        }
    }
}