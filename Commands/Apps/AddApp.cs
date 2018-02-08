using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Apps
{
    [Cmdlet(VerbsCommon.Add, "App", DefaultParameterSetName="Test")]
    [CmdletHelp("Add/uploads an available app to the app catalog",
        Category = CmdletHelpCategory.Apps, SupportedPlatform = CmdletSupportedPlatform.Online,
        OutputType = typeof(AppMetadata),
        Verb=VerbsCommon.Add,
        Noun = "App")]
    [CmdletExample(
        Code = @"PS:> Add-PnPApp -Path ./myapp.sppkg",
        Remarks = @"This will upload the specified app package to the app catalog", SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Add-PnPApp -Path ./myapp.sppkg -Publish",
        Remarks = @"This will upload the specified app package to the app catalog and deploy/trust it at the same time.", SortOrder = 2)]
    public class AddApp : PnPCmdlet
    {
        private const string ParameterSet_ADD = "Add only";
        private const string ParameterSet_PUBLISH = "Add and Publish";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ADD, ValueFromPipeline = true, HelpMessage = "Specifies the Id or an actual app metadata instance")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_PUBLISH, ValueFromPipeline = true, HelpMessage = "Specifies the Id or an actual app metadata instance")]
        public string Path;

        [Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = ParameterSet_PUBLISH, HelpMessage = "This will deploy/trust an app into the app catalog")]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = ParameterSet_PUBLISH)]
        public SwitchParameter SkipFeatureDeployment;

        [Parameter(Mandatory = false, HelpMessage = "Overwrites the existing app package if it already exists")]
        public SwitchParameter Overwrite;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var fileInfo = new System.IO.FileInfo(Path);

            var bytes = System.IO.File.ReadAllBytes(Path);

            var manager = new AppManager(CurrentContext);

            var result = manager.Add(bytes, fileInfo.Name, Overwrite);

            try
            {

                if (Publish)
                {
                    if (manager.Deploy(result, SkipFeatureDeployment))
                    {
                        result = manager.GetAvailable(result.Id);
                    }

                }
                WriteObject(result);
            }
            catch
            {
                // Exception occurred rolling back
                manager.Remove(result);
                throw;
            }
        }
    }
}