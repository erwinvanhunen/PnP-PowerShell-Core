using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ClientSidePages
{
    [Cmdlet(VerbsCommon.Get, "AvailableClientSideComponents")]
    [CmdletHelp(VerbsCommon.Get,"AvailableClientSideComponents","Gets the available client side components on a particular page",
      Category = CmdletHelpCategory.ClientSidePages, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
        Code = @"PS:> Get-PnPAvailableClientSideComponents",
        Remarks = "Gets the list of available client side components on the page 'MyPage.aspx'",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPAvailableClientSideComponents -ComponentName ""HelloWorld""",
        Remarks = "Gets the client side component 'HelloWorld'",
        SortOrder = 3)]
    public class GetAvailableClientSideComponents : PnPCmdlet
    {
        [Obsolete("This parameter is not required anymore")]
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "The name of the page.")]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = false, HelpMessage = "Specifies the component instance or Id to look for.")]
        public ClientSideComponentPipeBind Component;

        protected override void ExecuteCmdlet()
        {
            if (Component == null)
            {
                var allComponents = new RestRequest(CurrentContext, "Web/GetClientSideWebParts").Get<ResponseCollection<ClientSideComponent>>().Items.Where(c => c.ComponentType == 1);
                WriteObject(allComponents, true);
            }
            else
            {
                WriteObject(Component.GetComponent(CurrentContext));
            }
        }
    }
}