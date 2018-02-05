using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.Get, "Web")]
    [CmdletHelp(VerbsCommon.Get,"Web","Returns the current web object",
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
        Code = @"PS:> Get-PnPWeb",
        Remarks = "This will return the current web",
        SortOrder = 1)]
    public class GetWeb : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(new RestRequest("Web").Expand("AllowAutomaticASPXPageIndexing", "AllowCreateDeclarativeWorkflowForCurrentUser","RequestAccessEmail","ServerRelativeUrl","SiteLogoUrl").Get<Model.Web>());
        }
    }
}