using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "HomePage")]
    public class GetHomePage : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest("Web").Expand("RootFolder/WelcomePage").Get<Model.Web>();

            if (string.IsNullOrEmpty(web.RootFolder.WelcomePage))
            {
                WriteObject("default.aspx");
            }
            else
            {
                WriteObject(web.RootFolder.WelcomePage);
            }
        }
    }
}
