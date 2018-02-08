using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Remove, "NavigationNode")]
    [CmdletHelp(VerbsCommon.Remove, "NavigationNode", "Returns navigation nodes for a web",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPNavigationNode -Identity 1032",
        Remarks = @"Removes the navigation node with the specified id",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> $nodes = Get-PnPNavigationNode -QuickLaunch
PS:>$nodes | Select-Object -First 1 | Remove-PnPNavigationNode -Force",
        Remarks = @"Retrieves all navigation nodes from the Quick Launch navigation, then removes the first node in the list and it will not ask for a confirmation",
        SortOrder = 2)]

    public class RemoveNavigationNode : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The Id or node object to delete")]
        public NavigationNodePipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.", ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("Remove node?", "Confirm"))
            {
                new RestRequest(Context, $"Web/Navigation/GetNodeById({Identity.Id})").Delete();
            }
        }
    }
}
