using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Web
{
	[OutputType(typeof(Model.Web))]
    [Cmdlet(VerbsCommon.Get, "Web")]
    public class GetWeb : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(new RestRequest("Web").Expand("AllowAutomaticASPXPageIndexing","AllowCreateDeclarativeWorkflowForCurrentUser").Get<Model.Web>());
        }
    }
}