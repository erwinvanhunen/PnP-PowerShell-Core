using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;
using System.Linq;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.Get, "SubWeb")]
    [CmdletAlias("Get-SubWebs")]
    [CmdletHelp(VerbsCommon.Get, "SubWeb", "Returns the subwebs of the current web",
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
        Code = @"PS:> Get-PnPSubWebs",
        Remarks = "This will return all sub webs for the current web",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPSubWebs -recurse",
        Remarks = "This will return all sub webs for the current web and its sub webs",
        SortOrder = 2)]
    public class GetSubWeb : PnPCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "include subweb of the subwebs")]
        public SwitchParameter Recurse;

        protected override void ExecuteCmdlet()
        {
            var webs = new RestRequest("Web/Webs").Expand("AllowAutomaticASPXPageIndexing", "AllowCreateDeclarativeWorkflowForCurrentUser", "RequestAccessEmail", "ServerRelativeUrl").Get<ResponseCollection<Model.Web>>().Items;
            if (!Recurse)
            {
                WriteObject(webs, true);
            }
            else
            {
                var subwebs = new List<Model.Web>();
                subwebs.AddRange(webs);
                foreach (var subweb in webs)
                {
                    subwebs.AddRange(GetSubWebsInternal(subweb));
                }
                WriteObject(subwebs.OrderBy(w => w.ServerRelativeUrl), true);
            }
        }

        private System.Collections.Generic.List<Model.Web> GetSubWebsInternal(Model.Web subweb)
        {
            var subwebs = new System.Collections.Generic.List<Model.Web>();
            var webs = new RestRequest($"{subweb.Url}/_api/Web/Webs").Get<ResponseCollection<Model.Web>>().Items;
            if (webs.Any())
            {
                subwebs.AddRange(webs);
                foreach (var sw in webs)
                {
                    subwebs.AddRange(GetSubWebsInternal(sw));
                }
            }
            return subwebs;
        }
    }


}