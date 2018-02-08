using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "NavigationNode", DefaultParameterSetName = ParameterSet_ALLBYLOCATION)]
    [CmdletHelp(VerbsCommon.Get, "NavigationNode", "Returns navigation nodes for a web",
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
        Code = @"PS:> Get-PnPNavigationNode -Id 2030",
        Remarks = @"Returns the selected navigation node and retrieves any children",
        SortOrder = 3)]
    public class GetNavigationNode : PnPCmdlet
    {
        private const string ParameterSet_ALLBYLOCATION = "All nodes by location";
        private const string ParameterSet_BYID = "A single node by ID";

        [Parameter(Mandatory = false, HelpMessage = "The location of the nodes to retrieve. Either TopNavigationBar, QuickLaunch", ParameterSetName = ParameterSet_ALLBYLOCATION)]
        public NavigationType Location = NavigationType.QuickLaunch;

        [Parameter(Mandatory = false, HelpMessage = "The Id of the node to retrieve", ParameterSetName = ParameterSet_BYID)]
        public int Id;

        protected override void ExecuteCmdlet()
        {

            if (ParameterSetName == ParameterSet_ALLBYLOCATION)
            {
                switch (Location)
                {
                    case NavigationType.QuickLaunch:
                        {
                            WriteObject(new RestRequest(Context, "Web/Navigation/Quicklaunch").Get<ResponseCollection<NavigationNode>>().Items);
                            break;
                        }
                    case NavigationType.TopNavigationBar:
                        {
                            WriteObject(new RestRequest(Context, "Web/Navigation/TopNavigationBar").Get<ResponseCollection<NavigationNode>>().Items, true);
                            break;
                        }
                    case NavigationType.SearchNav:
                        {
                            WriteObject(new RestRequest(Context, "Web/Navigation/GetNodeById(1040)").Get<NavigationNode>(), true);
                            break;
                        }
                }

            }
            if (MyInvocation.BoundParameters.ContainsKey("Id"))
            {
                WriteObject(new RestRequest(Context, $"/Web/Navigation/GetNodeById({Id})").Expand("Children").Get<NavigationNode>());
            }
        }
    }
}
