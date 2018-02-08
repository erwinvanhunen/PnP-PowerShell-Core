using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsData.Publish, "App")]
    [CmdletHelp(VerbsData.Publish,"App","Publishes/Deploys/Trusts an available app in the app catalog",
        Category = CmdletHelpCategory.Apps, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Publish-PnPApp", Remarks = @"This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the app catalog", SortOrder = 1)]

    public class PublishApp : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Specifies the Id of the app")]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipFeatureDeployment;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(CurrentContext);

            manager.Deploy(Identity.GetId(), SkipFeatureDeployment);
        }
    }
}