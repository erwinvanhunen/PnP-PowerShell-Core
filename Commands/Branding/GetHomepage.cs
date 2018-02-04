using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "HomePage")]
     [CmdletHelp(VerbsCommon.Get, "HomePage","Returns the URL to the page set as home page", 
        Category = CmdletHelpCategory.Branding,
        OutputType = typeof(string))]
    [CmdletExample(Code = @"PS:> Get-PnPHomePage",
        Remarks = "Will return the URL of the home page of the web.",
        SortOrder = 1)]
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
