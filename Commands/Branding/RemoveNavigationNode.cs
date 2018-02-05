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
        Code = @"PS:> Get-PnPNavigationNode",
        Remarks = @"Returns all navigation nodes in the quicklaunch navigation",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPNavigationNode -QuickLaunch",
        Remarks = @"Returns all navigation nodes in the quicklaunch navigation",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Get-PnPNavigationNode -TopNavigationBar",
        Remarks = @"Returns all navigation nodes in the top navigation bar",
        SortOrder = 3)]
    [CmdletExample(
        Code = @"PS:> Get-PnPNavigationNode -Id 1025",
        Remarks = @"Returns the navigation node with id 1025",
        SortOrder = 4)]
    public class RemoveNavigationNode : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The Id or node object to delete")]
        public NavigationNodePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            new RestRequest(Context, $"Web/Navigation/GetNodeById({Identity.Id})").Delete();
        }
    }
}
