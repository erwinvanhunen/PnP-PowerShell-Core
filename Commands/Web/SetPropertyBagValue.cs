using System;
using System.Linq;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Extensions;
using SharePointPnP.PowerShell.Core.Helpers;

namespace SharePointPnP.PowerShell.Core
{
    [Cmdlet(VerbsCommon.Set, "PropertyBagValue")]
    [CmdletHelp(VerbsCommon.Set, "PropertyBagValue", "Sets a property bag value",
        Category = CmdletHelpCategory.Webs)]
    [CmdletExample(
      Code = @"PS:> Set-PnPPropertyBagValue -Key MyKey -Value MyValue",
      Remarks = "This sets or adds a value to the current web property bag",
      SortOrder = 1)]
    [CmdletExample(
      Code = @"PS:> Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /",
      Remarks = "This sets or adds a value to the root folder of the current web",
      SortOrder = 2)]
    [CmdletExample(
      Code = @"PS:> Set-PnPPropertyBagValue -Key MyKey -Value MyValue -Folder /MyFolder",
      Remarks = "This sets or adds a value to the folder MyFolder which is located in the root folder of the current web",
      SortOrder = 3)]
    public class SetPropertyBagValue : PnPCmdlet
    {
        private const string ParameterSet_WEB = "Web";
        private const string ParameterSet_FOLDER = "Folder";
        
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_WEB)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FOLDER)]
        public string Key;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_WEB)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FOLDER)]
        [Parameter(Mandatory = true)]
        public string Value;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_WEB)]
        public SwitchParameter Indexed;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDER, HelpMessage = "Site relative url of the folder. See examples for use.")]
        public string Folder;

        protected override void ExecuteCmdlet()
        {
            var web = new RestRequest("Web").Get<Model.Web>();
            var site = new RestRequest("Site").Get<Site>();
            if (web.IsNoScriptSite())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Site has NoScript enabled, and setting property bag values is not supported"), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                return;
            }
            if (!MyInvocation.BoundParameters.ContainsKey("Folder"))
            {
                if (!Indexed)
                {
                    // If it is already an indexed property we still have to add it back to the indexed properties
                    //Indexed = !string.IsNullOrEmpty(SelectedWeb.GetIndexedPropertyBagKeys().FirstOrDefault(k => k == Key));
                }

                var payload = $@"<Request AddExpandoFieldTypeSuffix=""true"" SchemaVersion=""15.0.0.0"" LibraryVersion=""16.0.0.0"" ApplicationName=""SharePoint PnP PowerShell Core"" xmlns=""http://schemas.microsoft.com/sharepoint/clientquery/2009"">
                    <Actions>
                        <Method Name=""SetFieldValue"" Id=""1"" ObjectPathId=""3"">
                            <Parameters>
                                <Parameter Type=""String"">{Key}</Parameter>
                                <Parameter Type=""String"">{Value}</Parameter>
                            </Parameters>
                        </Method>
                        <Method Name=""Update"" Id=""2"" ObjectPathId=""4"" />
                    </Actions>
                    <ObjectPaths>
                        <Property Id=""3"" ParentId=""4"" Name=""AllProperties"" />
                        <Identity Id=""4"" Name=""e82e479e-4047-5000-d2b3-60e4a5a07d64|740c6a0b-85e2-48a0-a494-e0f1759d4aa7:site:{site.Id}:web:{web.Id}"" />
                    </ObjectPaths>
                </Request>";
                ClientSvcHelper.Execute(payload);
            }
            else
            {
                var serverRelativeUrl = web.ServerRelativeUrl;

                var folderUrl = UrlUtility.Combine(serverRelativeUrl, Folder);

                var folder = new RestRequest($"Web/GetFolderByServerRelativePath(decodedurl='/{folderUrl}')").Select("UniqueId").Get<Folder>();

                var payload = $@"<Request AddExpandoFieldTypeSuffix=""true"" SchemaVersion=""15.0.0.0"" LibraryVersion=""16.0.0.0"" ApplicationName=""SharePoint PnP PowerShell Core"" xmlns=""http://schemas.microsoft.com/sharepoint/clientquery/2009"">
                    <Actions>
                        <Method Name=""SetFieldValue"" Id=""1"" ObjectPathId=""3"">
                            <Parameters>
                                <Parameter Type=""String"">{Key}</Parameter>
                                <Parameter Type=""String"">{Value}</Parameter>
                            </Parameters>
                        </Method>
                        <Method Name=""Update"" Id=""2"" ObjectPathId=""4"" />
                    </Actions>
                    <ObjectPaths>
                        <Property Id=""3"" ParentId=""4"" Name=""Properties"" />
                        <Identity Id=""4"" Name=""e82e479e-4047-5000-d2b3-60e4a5a07d64|740c6a0b-85e2-48a0-a494-e0f1759d4aa7:site:{site.Id}:web:{web.Id}:folder:{folder.UniqueId}"" />
                    </ObjectPaths>
                </Request>";
                ClientSvcHelper.Execute(payload);
            }
        }
    }
}
