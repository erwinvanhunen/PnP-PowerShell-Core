using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.Get, "Context")]
    [CmdletHelp(VerbsCommon.Get, "Context", "Returns the current context",
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
        Code = @"PS:> $context = Get-PnPContext",
        Remarks = "This will return the current context",
        SortOrder = 1)]
    public class Getcontext : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(Context);
        }
    }
}