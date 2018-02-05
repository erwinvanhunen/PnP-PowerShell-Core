using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.New, "Web")]
    [CmdletHelp(VerbsCommon.New, "Web", "Creates a new subweb under the current web",
       Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
       Code = @"PS:> New-PnPWeb -Title ""Project A Web"" -Url projectA -Description ""Information about Project A"" -Locale 1033 -Template ""STS#0""",
       Remarks = "Creates a new subweb under the current web with URL projectA",
       SortOrder = 1)]
    public class NewWeb : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The title of the new web")]
        public string Title;

        [Parameter(Mandatory = true, HelpMessage = "The URL of the new web")]
        public string Url;

        [Parameter(Mandatory = false, HelpMessage = "The description of the new web")]
        public string Description = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "The language id of the new web. default = 1033 for English")]
        public int Locale = 1033;

        [Parameter(Mandatory = true, HelpMessage = "The site definition template to use for the new web, e.g. STS#0. Use Get-PnPWebTemplates to fetch a list of available templates")]
        public string Template = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "By default the subweb will inherit its security from its parent, specify this switch to break this inheritance")]
        public SwitchParameter BreakInheritance = false;

        [Parameter(Mandatory = false, HelpMessage = "Specifies whether the site inherits navigation.")]
        public SwitchParameter InheritNavigation = true;
        protected override void ExecuteCmdlet()
        {
            var properties = new Dictionary<string, object>();
            properties["Url"] = Url;
            properties["Title"] = Title;
            properties["Description"] = Description;
            properties["WebTemplate"] = Template;
            properties["Language"] = Locale;
            properties["UseSamePermissionsAsParentSite"] = !BreakInheritance.IsPresent;

            var web = new RestRequest(Context, "Web/Webs/add").Post<Model.Web>(new MetadataType("SP.WebCreationInformation"), properties);

            if (InheritNavigation == false)
            {
                new RestRequest(Context, $"{web.Url}/_api/Navigation").Merge(new MetadataType("SP.Navigation"), new Dictionary<string, object>() {
                { "UseShared", false}});
            }
            WriteObject(web);

        }

    }
}