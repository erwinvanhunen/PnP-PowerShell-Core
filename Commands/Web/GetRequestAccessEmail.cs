using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.Get, "RequestAccessEmail")]
    [CmdletHelp(VerbsCommon.Get, "RequestAccessEmail", "Returns the request access e-mail addresses",
        SupportedPlatform = CmdletSupportedPlatform.Online,
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
        Code = @"PS:> Get-PnPRequestAccessEmails",
        Remarks = "This will return all the request access e-mail addresses for the current web",
        SortOrder = 1)]
    public class GetRequestAccessEmail : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest(CurrentContext, "Web").Expand("RequestAccessEmail").Get<Model.Web>();
            WriteObject(web.RequestAccessEmail.Split(new char[] { ',' }), true);
        }
    }
}