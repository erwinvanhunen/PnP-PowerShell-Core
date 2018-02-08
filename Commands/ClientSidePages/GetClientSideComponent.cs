using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ClientSidePages
{
    [Cmdlet(VerbsCommon.Get, "ClientSideComponent")]
    [CmdletHelp(VerbsCommon.Get,"ClientSideComponent","Retrieve one or more Client-Side components from a page",
        Category = CmdletHelpCategory.WebParts,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
        Code = @"PS:> Get-PnPClientSideComponent -Page Home",
        Remarks = @"Returns all controls defined on the given page.", SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPClientSideComponent -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82",
        Remarks = @"Returns a specific control defined on the given page.", SortOrder = 2)]
    public class GetClientSideControl : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The name of the page")]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The instance id of the component")]
        public GuidPipeBind InstanceId;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Context);

            if (clientSidePage == null)
                throw new Exception($"Page '{Page?.Name}' does not exist");

            if (!MyInvocation.BoundParameters.ContainsKey("InstanceId"))
            {
                WriteObject(clientSidePage.Controls, true);
            }
            else
            {
                WriteObject(clientSidePage.Controls.FirstOrDefault(c => c.InstanceId == InstanceId.Id));
            }

        }
    }
}
