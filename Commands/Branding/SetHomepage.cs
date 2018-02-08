using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Set, "HomePage")]
    [CmdletHelp(VerbsCommon.Set, "HomePage", "Sets the home page of the current web.",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(
        Code = @"PS:> Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx",
        Remarks = "Sets the home page to the home.aspx file which resides in the SitePages library",
        SortOrder = 1)]
    public class SetHomePage : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The root folder relative url of the homepage, e.g. 'sitepages/home.aspx'", Position = 0, ValueFromPipeline = true)]
        public string RootFolderRelativeUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var dict = new Dictionary<string, object>()
            {
                {"WelcomePage",RootFolderRelativeUrl }
            };

            new RestRequest(Context, "Web/RootFolder").Merge(new MetadataType("SP.Folder"), dict);
        }
    }
}
