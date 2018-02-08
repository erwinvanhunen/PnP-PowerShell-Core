using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Set, "NavigationNode")]
    [CmdletHelp(VerbsCommon.Set, "NavigationNode", "Sets the home page of the current web.",
     Category = CmdletHelpCategory.Branding)]
    [CmdletExample(
     Code = @"PS:> Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx",
     Remarks = "Sets the home page to the home.aspx file which resides in the SitePages library",
     SortOrder = 1)]
    public class SetNavigationNode : PnPCmdlet
    {
        [Parameter(Mandatory = true)]
        public NavigationNodePipeBind Identity;

        [Parameter(Mandatory = false)]
        public int NewId;

        [Parameter(Mandatory = false, HelpMessage = "The title of the node")]
        public string Title;

        [Parameter(Mandatory = false, HelpMessage = "The url to navigate to when clicking the node")]
        public string Url;

        [Parameter(Mandatory = false, HelpMessage = "Defines if the node is visible is not")]
        public bool IsVisible;

        [Parameter(Mandatory = false, HelpMessage = "Defines if the node links to an external URL")]
        public bool IsExternal;

        protected override void ExecuteCmdlet()
        {
            var dict = new Dictionary<string, object>();
            if(MyInvocation.BoundParameters.ContainsKey("NewId"))
            {
                dict.Add("Id", NewId);
            }
            if (MyInvocation.BoundParameters.ContainsKey("Title"))
            {
                dict.Add("Title", Title);
            }
            if (MyInvocation.BoundParameters.ContainsKey("Url"))
            {
                dict.Add("Url", Url);
            }
            if (MyInvocation.BoundParameters.ContainsKey("IsVisible"))
            {
                dict.Add("IsVisible", IsVisible);
            }
            if (MyInvocation.BoundParameters.ContainsKey("IsExternal"))
            {
                dict.Add("IsExternal", IsExternal);
            }
            new RestRequest(CurrentContext, $"Web/Navigation/GetNodeById({Identity.Id})").Merge(new MetadataType("SP.NavigationNode"), dict);


        }
    }
}
