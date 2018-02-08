using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Helpers;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsCommon.Remove, "App")]
    [CmdletHelp(VerbsCommon.Remove,"App","Removes an app from the app catalog", SupportedPlatform = CmdletSupportedPlatform.Online,
        Category = CmdletHelpCategory.Apps)]
    [CmdletExample(Code = @"PS:> Remove-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", Remarks = @"This will remove the specified app from the app catalog", SortOrder = 1)]
    public class RemoveApp : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Specifies the Id of the Addin Instance")]
        public AppMetadataPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(Context);

            manager.Remove(Identity.GetId());
        }
    }
}