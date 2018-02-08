using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsLifecycle.Uninstall, "App")]
    [CmdletHelp(VerbsLifecycle.Uninstall, "App", "Uninstalls an available add-in from the site",
        Category = CmdletHelpCategory.Apps)]
    [CmdletExample(Code = @"PS:> Uninstall-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", Remarks = @"This will uninstall the specified app from the current site.", SortOrder = 2)]
    public class UninstallApp : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Specifies the Id of the Addin Instance")]
        public AppMetadataPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(Context);

            manager.Uninstall(Identity.GetId());
        }
    }
}
