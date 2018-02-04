using System.Linq;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections;
using System.Collections.Generic;
using SharePointPnP.PowerShell.Core.Helpers;

namespace SharePointPnP.PowerShell.Core.Web
{
    [Cmdlet(VerbsCommon.Get, "PropertyBag")]
    [CmdletHelp(VerbsCommon.Get, "PropertyBag", "Returns the property bag values.",
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
       Code = @"PS:> Get-PnPPropertyBag",
       Remarks = "This will return all web property bag values",
       SortOrder = 1)]
    [CmdletExample(
       Code = @"PS:> Get-PnPPropertyBag -Key MyKey",
       Remarks = "This will return the value of the key MyKey from the web property bag",
       SortOrder = 2)]
    [CmdletExample(
       Code = @"PS:> Get-PnPPropertyBag -Folder /MyFolder",
       Remarks = "This will return all property bag values for the folder MyFolder which is located in the root of the current web",
       SortOrder = 3)]
    [CmdletExample(
       Code = @"PS:> Get-PnPPropertyBag -Folder /MyFolder -Key vti_mykey",
       Remarks = "This will return the value of the key vti_mykey from the folder MyFolder which is located in the root of the current web",
       SortOrder = 4)]
    [CmdletExample(
        Code = @"PS:> Get-PnPPropertyBag -Folder / -Key vti_mykey",
        Remarks = "This will return the value of the key vti_mykey from the root folder of the current web",
        SortOrder = 5)]
    public class GetPropertyBag : PnPCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, HelpMessage = "Key that should be looked up")]
        public string Key = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "Site relative url of the folder. See examples for use.")]
        public string Folder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                var properties = new RestRequest("Web/AllProperties").Get<Dictionary<string, string>>().Where(k => !k.Key.StartsWith("odata.")).Select(p => new PropertyBagValue() { Key = p.Key.Replace("_x005f_", "_").Replace("OData_", ""), Value = p.Value });
                if (!string.IsNullOrEmpty(Key))
                {
                    WriteObject(properties.FirstOrDefault(p => p.Key == Key));
                }
                else
                {
                    WriteObject(properties, true);
                }
            }
            else
            {
                // Folder Property Bag

                var web = new RestRequest("Web").Select("ServerRelativeUrl").Get<Model.Web>();

                var serverRelativeUrl = web.ServerRelativeUrl;

                var folderUrl = UrlUtility.Combine(serverRelativeUrl, Folder);

                var folderProperties = new RestRequest($"Web/GetFolderByServerRelativePath(decodedurl='/{folderUrl}')/Properties").Get<Dictionary<string, string>>()
                    .Where(k => !k.Key.StartsWith("odata."))
                    .Select(p => new PropertyBagValue() { Key = p.Key.Replace("_x005f_", "_").Replace("OData_", ""), Value = p.Value });

                if (!string.IsNullOrEmpty(Key))
                {
                    WriteObject(folderProperties.FirstOrDefault(p => p.Key == Key));
                }
                else
                {
                    WriteObject(folderProperties, true);
                }
            }
        }
    }

    public class PropertyBagValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
