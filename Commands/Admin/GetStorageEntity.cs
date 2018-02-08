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
    [Cmdlet(VerbsCommon.Get, "StorageEntity", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Get, "StorageEntity", @"Retrieve Storage Entities / Farm Properties.",
        Category = CmdletHelpCategory.TenantAdmin,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Get-PnPStorageEntity", Remarks = "Returns all site storage entities/farm properties", SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Get-PnPTenantSite -Key MyKey", Remarks = "Returns the storage entity/farm property with the given key.", SortOrder = 2)]
    public class GetStorageEntity : PnPCmdlet
    {
        const string ParameterSetAllEntities = "All Entities";

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID, name or Url (Lists/MyList) of the list.")]
        public string Key;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID, name or Url (Lists/MyList) of the list.", ParameterSetName = ParameterSetAllEntities)]
        public string AppCatalogUrl;

        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.BoundParameters.ContainsKey("Key"))
            {
                var entity = new RestRequest(CurrentContext, $"web/GetStorageEntity(Key='{Key}')").Get<StorageEntity>();
                if (entity != null)
                {
                    entity.Key = Key;
                }
                WriteObject(entity);
            }
            else
            {
                var appCatalogUrl = AppCatalogUrl;
                if (string.IsNullOrEmpty(AppCatalogUrl))
                {
                    appCatalogUrl = AppManager.GetAppCatalogUrl(CurrentContext);
                }
                var properties = new RestRequest(CurrentContext, $"{appCatalogUrl}/_api/Web/AllProperties").Get<ClientSideDictionary<string, string>>();
                if (properties.ContainsKey("storageentitiesindex"))
                {
                    var storageEntitiesDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(properties["storageentitiesindex"]);
                    var storageEntities = new List<StorageEntity>();
                    foreach (var key in storageEntitiesDict.Keys)
                    {
                        var storageEntity = new StorageEntity
                        {
                            Key = key,
                            Value = storageEntitiesDict[key]["Value"],
                            Comment = storageEntitiesDict[key]["Comment"],
                            Description = storageEntitiesDict[key]["Description"]
                        };
                        storageEntities.Add(storageEntity);
                    }
                    WriteObject(storageEntities, true);
                }
            }
        }
    }
}