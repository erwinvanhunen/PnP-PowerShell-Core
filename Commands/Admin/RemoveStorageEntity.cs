using System.Management.Automation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Attributes;
using System.Linq;

namespace SharePointPnP.PowerShell.Core.Admin
{
    [Cmdlet(VerbsCommon.Remove, "StorageEntity", SupportsShouldProcess = true)]
    [CmdletHelp(@"Remove Storage Entities / Farm Properties.",
        Category = CmdletHelpCategory.TenantAdmin,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Remove-PnPStorageEntity -Key MyKey ", Remarks = "Removes an existing storage entity / farm property", SortOrder = 1)]
    public class RemoveStorageEntity : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The key of the value to set.")]
        public string Key;

        protected override void ExecuteCmdlet()
        {
            var appCatalogUrl = AppManager.GetAppCatalogUrl();
            new RestRequest($"{appCatalogUrl}/_api/Web/RemoveStorageEntity(key='{Key}')").Post();
        }
    }
}