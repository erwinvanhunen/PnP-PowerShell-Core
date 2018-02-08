using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ClientSidePages
{
    [Cmdlet(VerbsCommon.Get, "ClientSidePage")]
    [CmdletHelp(VerbsCommon.Get,"ClientSidePage","Gets a Client-Side Page",
      Category = CmdletHelpCategory.ClientSidePages, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
        Code = @"PS:> Get-PnPClientSidePage -Identity ""MyPage.aspx""",
        Remarks = "Gets the Modern Page (Client-Side) named 'MyPage.aspx' in the current SharePoint site",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Get-PnPClientSidePage ""MyPage""",
        Remarks = "Gets the Modern Page (Client-Side) named 'MyPage.aspx' in the current SharePoint site",
        SortOrder = 2)]
    public class GetClientSidePage : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The name of the page")]
        public ClientSidePagePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Identity.GetPage(Context);

            if (clientSidePage == null)
                throw new Exception($"Page '{Identity?.Name}' does not exist");

            WriteObject(clientSidePage);
        }
    }
}
