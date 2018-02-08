using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Set, "MinimalDownloadStrategy")]
    [CmdletHelp(VerbsCommon.Set,"MinimalDownloadStrategy","Activates or deactivates the minimal downloading strategy.",
        "Activates or deactivates the minimal download strategy feature of a site",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMinimalDownloadStrategy -Off",
        Remarks = "Will deactivate minimal download strategy (MDS) for the current web.",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMinimalDownloadStrategy -On",
        Remarks = "Will activate minimal download strategy (MDS) for the current web.",
        SortOrder = 2)]
    public class SetMinimalDownloadStrategy : PnPCmdlet
    {
        [Parameter(ParameterSetName = "On", Mandatory = true, HelpMessage = "Turn minimal download strategy on")]
        public SwitchParameter On;

        [Parameter(ParameterSetName = "Off", Mandatory = true, HelpMessage = "Turn minimal download strategy off")]
        public SwitchParameter Off;

        protected override void ExecuteCmdlet()
        {
            if (On)
            {
                new RestRequest(CurrentContext,$"Web/Features/Add(guid'{Constants.FeatureId_Web_MinimalDownloadStrategy}')").Post();
            }
            else
            {
                new RestRequest(CurrentContext, $"Web/Features/Remove(guid'{Constants.FeatureId_Web_MinimalDownloadStrategy}')").Post();
            }
        }
    }

}
