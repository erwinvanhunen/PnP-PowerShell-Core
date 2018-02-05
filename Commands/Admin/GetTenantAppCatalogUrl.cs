using System.Linq;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Helpers;

namespace SharePointPnP.PowerShell.Core.Admin
{
    [Cmdlet(VerbsCommon.Get, "TenantAppCatalogUrl", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Get,"TenantAppCatalogUrl","Retrieves the url of the tenant scoped app catalog.",
        Category = CmdletHelpCategory.TenantAdmin,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Get-PnPTenantAppCatalogUrl", Remarks = "Returns the url of the tenant scoped app catalog site collection", SortOrder = 1)]
    public class GetTenantAppCatalogUrl : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(AppManager.GetAppCatalogUrl(Context));
        }
    }
}