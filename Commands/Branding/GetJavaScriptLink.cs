using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "JavaScriptLink")]
    [CmdletHelp(VerbsCommon.Get, "JavaScriptLink","Returns all or a specific custom action(s) with location type ScriptLink", 
        Category = CmdletHelpCategory.Branding,
        OutputType = typeof(UserCustomAction),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.usercustomaction.aspx")]
    [CmdletExample(Code = "PS:> Get-PnPJavaScriptLink",
                   Remarks = "Returns all web scoped JavaScript links",
                   SortOrder = 1)]
    [CmdletExample(Code = "PS:> Get-PnPJavaScriptLink -Scope All",
                   Remarks = "Returns all web and site scoped JavaScript links",
                   SortOrder = 2)]
    [CmdletExample(Code = "PS:> Get-PnPJavaScriptLink -Scope Web",
                   Remarks = "Returns all Web scoped JavaScript links",
                   SortOrder = 3)]
    [CmdletExample(Code = "PS:> Get-PnPJavaScriptLink -Scope Site",
                   Remarks = "Returns all Site scoped JavaScript links",
                   SortOrder = 4)]
    [CmdletExample(Code = "PS:> Get-PnPJavaScriptLink -Name Test",
                   Remarks = "Returns the web scoped JavaScript link named Test",
                   SortOrder = 5)]
    public class GetJavaScriptLink : PnPCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "Name of the Javascript link. Omit this parameter to retrieve all script links")]
        public string Name = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "Scope of the action, either Web, Site or All to return both, defaults to Web")]
        public CustomActionScope Scope = CustomActionScope.Web;

        protected override void ExecuteCmdlet()
        {
            var actions = new List<UserCustomAction>();

            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
            {
                actions.AddRange(new RestRequest("Web/UserCustomActions").Get<ResponseCollection<UserCustomAction>>().Items.Where(c => c.Location == "ScriptLink"));
            }
            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
            {
                actions.AddRange(new RestRequest("Site/UserCustomActions").Get<ResponseCollection<UserCustomAction>>().Items.Where(c => c.Location == "ScriptLink"));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                var foundAction = actions.FirstOrDefault(x => x.Title == Name);
                if (foundAction != null)
                {
                    WriteObject(foundAction, true);
                }
                else
                {
                    throw new PSArgumentException($"No JavaScriptLink found with the name '{Name}' within the scope '{Scope}'", "Name");
                }
            }
            else
            {
                WriteObject(actions, true);
            }
        }
    }
}