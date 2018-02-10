using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Linq.Expressions;
using System.Management.Automation;


namespace SharePointPnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "Folder")]
    [CmdletHelp(VerbsCommon.Get,"Folder","Return a folder object", Category = CmdletHelpCategory.Files,
        DetailedDescription = "Retrieves a folder if it exists. Use Ensure-PnPFolder to create the folder if it does not exist.")]
    [CmdletExample(
        Code = @"PS:> Get-PnPFolder -RelativeUrl ""Shared Documents""",
        Remarks = "Returns the folder called 'Shared Documents' which is located in the root of the current web",
        SortOrder = 1
        )]
    [CmdletExample(
        Code = @"PS:> Get-PnPFolder -RelativeUrl ""/sites/demo/Shared Documents""",
        Remarks = "Returns the folder called 'Shared Documents' which is located in the root of the current web",
        SortOrder = 1
        )]
    [CmdletRelatedLink(
        Text = "Ensure-PnPFolder",
        Url = "https://github.com/OfficeDev/PnP-PowerShell/blob/master/Documentation/EnsureSPOFolder.md")]
    public class GetFolder : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "Site or server relative URL of the folder to retrieve. In the case of a server relative url, make sure that the url starts with the managed path as the current web.")]
        public string Url;

        protected override void ExecuteCmdlet()
        {
            var webServerRelativeUrl = Context.Web.ServerRelativeUrl;
            if (!Url.ToLower().StartsWith(webServerRelativeUrl.ToLower()))
            {
                Url = UrlUtility.Combine(webServerRelativeUrl, Url);
            }
            var folder = new RestRequest(Context, $"{Context.Web.ObjectPath}/GetFolderByServerRelativeUrl('{Url}')").Get<Folder>();

            WriteObject(folder);
        }
    }
}
