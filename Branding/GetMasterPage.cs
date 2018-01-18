using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "MasterPage")]

    public class GetMasterPage : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest("Web").Select("MasterUrl","CustomMasterUrl").Get<Model.Web>();

            WriteObject(new { web.MasterUrl, web.CustomMasterUrl });
        }
    }
}
