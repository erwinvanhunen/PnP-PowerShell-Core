using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsData.Unpublish, "App")]
    [CmdletHelp(VerbsData.Unpublish,"App","Unpublishes/retracts an available add-in from the app catalog",
        Category = CmdletHelpCategory.Apps, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Unpublish-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", Remarks = @"This will retract, but not remove, the specified app from the app catalog", SortOrder = 2)]
    public class UnpublishApp : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Specifies the Id of the Addin Instance")]
        public AppMetadataPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(Context);

            manager.Retract(Identity.GetId());
        }
    }
}