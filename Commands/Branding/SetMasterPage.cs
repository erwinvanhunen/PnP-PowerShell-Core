using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Extensions;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Set, "MasterPage")]
    [CmdletHelp(VerbsCommon.Set, "MasterPage", "Set the masterpage",
        "Sets the default master page of the current web.",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master",
        Remarks = "Sets the master page based on a server relative URL",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master -CustomMasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master",
        Remarks = "Sets the master page and custom master page based on a server relative URL",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master",
        Remarks = "Sets the master page based on a site relative URL",
        SortOrder = 3)]
    [CmdletExample(
        Code = @"PS:> Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master -CustomMasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master",
        Remarks = "Sets the master page and custom master page based on a site relative URL",
        SortOrder = 4)]
    public class SetMasterPage : PnPCmdlet
    {
        private const string ParameterSet_SERVER = "Server Relative";
        private const string ParameterSet_SITE = "Site Relative";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER, HelpMessage = "Specifies the Master page URL based on the server relative URL")]
        public string MasterPageServerRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER, HelpMessage = "Specifies the custom Master page URL based on the server relative URL")]
        public string CustomMasterPageServerRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE, HelpMessage = "Specifies the Master page URL based on the site relative URL")]
        public string MasterPageSiteRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE, HelpMessage = "Specifies the custom Master page URL based on the site relative URL")]
        public string CustomMasterPageSiteRelativeUrl = null;

        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest(Context,"Web").Get<Model.Web>();
            if (web.IsNoScriptSite())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Site has NoScript enabled, and setting custom master pages is not supported."), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                return;
            }

            if (ParameterSetName == ParameterSet_SERVER)
            {
                if (!string.IsNullOrEmpty(MasterPageServerRelativeUrl))
                {
                    web.MasterUrl = MasterPageServerRelativeUrl;
                    web.Update();
                }
                if (!string.IsNullOrEmpty(CustomMasterPageServerRelativeUrl))
                {
                    web.CustomMasterUrl = CustomMasterPageServerRelativeUrl;
                    web.Update();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(MasterPageSiteRelativeUrl))
                {
                    web.MasterUrl = UrlUtility.Combine(web.ServerRelativeUrl, MasterPageSiteRelativeUrl);
                    web.Update();
                }
                if (!string.IsNullOrEmpty(CustomMasterPageSiteRelativeUrl))
                {
                    web.CustomMasterUrl = UrlUtility.Combine(web.ServerRelativeUrl, CustomMasterPageSiteRelativeUrl);
                    web.Update();
                }
            }
        }
    }
}
