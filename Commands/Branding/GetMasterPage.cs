using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "MasterPage")]
    [CmdletHelp(VerbsCommon.Get, "MasterPage", "Returns the URLs of the default Master Page and the custom Master Page.",
        Category = CmdletHelpCategory.Branding)]
    public class GetMasterPage : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest("Web").Select("MasterUrl", "CustomMasterUrl").Get<Model.Web>();

            WriteObject(new { web.MasterUrl, web.CustomMasterUrl });
        }
    }
}
