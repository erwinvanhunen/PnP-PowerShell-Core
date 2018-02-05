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
    [Cmdlet(VerbsCommon.Set, "StorageEntity", SupportsShouldProcess = true)]
    [CmdletHelp(@"Set Storage Entities / Farm Properties.",
        Category = CmdletHelpCategory.TenantAdmin,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Set-PnPStorageEntity -Key MyKey -Value ""MyValue"" -Comment ""My Comment"" -Description ""My Description""", Remarks = "Sets an existing or adds a new storage entity / farm property", SortOrder = 1)]
    public class SetStorageEntity : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The key of the value to set.")]
        public string Key;

        [Parameter(Mandatory = true, HelpMessage = "The value to set.")]
        public string Value;

        [Parameter(Mandatory = true, HelpMessage = "The comment to set.")]
        public string Comment;

        [Parameter(Mandatory = true, HelpMessage = "The description to set.")]
        public string Description;

        protected override void ExecuteCmdlet()
        {
            var appcatalogurl = AppManager.GetAppCatalogUrl(Context);
            new RestRequest(Context, $"{appcatalogurl}/_api/Web/SetStorageEntity(key='{Key}',value='{Value}',comments='{Comment}',description='{Description}')").Post();
        }
    }
}