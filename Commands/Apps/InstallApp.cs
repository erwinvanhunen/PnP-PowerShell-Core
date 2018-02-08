using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsLifecycle.Install, "App")]
    [CmdletHelp(VerbsLifecycle.Install,"App","Installs an available app from the app catalog",
        Category = CmdletHelpCategory.Apps, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Install-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", Remarks = @"This will install an available app, specified by the id, to the current site.", SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Get-PnPAvailableApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe | Install-PnPApp", Remarks = @"This will install the given app into the site.", SortOrder = 2)]
    public class InstallApp : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Specifies the Id or an actual app metadata instance")]
        public AppMetadataPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(CurrentContext);

            manager.Install(Identity.GetId());
        }
    }
}
