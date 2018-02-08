using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Admin
{

    [Cmdlet(VerbsCommon.Get, "WebTemplate")]
    [CmdletAlias("Get-WebTemplates")]
    [CmdletHelp(VerbsCommon.Get, "WebTemplate", @"Returns the available web templates.",
        "Will list all available templates one can use to create a classic site.",
        Category = CmdletHelpCategory.TenantAdmin,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Get-PnPWebTemplates", SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Get-PnPWebTemplates -LCID 1033", Remarks = @"Returns all webtemplates for the Locale with ID 1033 (English)", SortOrder = 2)]
    [CmdletExample(Code = @"PS:> Get-PnPWebTemplates -CompatibilityLevel 15", Remarks = @"Returns all webtemplates for the compatibility level 15", SortOrder = 2)]
    [CmdletRelatedLink(Text = "Locale IDs", Url = "http://go.microsoft.com/fwlink/p/?LinkId=242911Id=242911")]
    public class GetWebTemplates : PnPCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "The language ID. For instance: 1033 for English")]
        public uint Lcid;

        [Parameter(Mandatory = false, HelpMessage = "The version of SharePoint")]
        public int CompatibilityLevel;

        protected override void ExecuteCmdlet()
        {
            var payload = @"<Request AddExpandoFieldTypeSuffix=""true"" SchemaVersion=""15.0.0.0"" LibraryVersion=""16.0.0.0"" ApplicationName="".NET Library"" xmlns=""http://schemas.microsoft.com/sharepoint/clientquery/2009"">
	<Actions>
		<ObjectPath Id=""2"" ObjectPathId=""1"" />
		<ObjectPath Id=""4"" ObjectPathId=""3"" />
		<Query Id=""5"" ObjectPathId=""3"">
			<Query SelectAllProperties=""true"">
				<Properties />
			</Query>
			<ChildItemQuery SelectAllProperties=""true"">
			<Properties /></ChildItemQuery></Query>
	</Actions>
	<ObjectPaths>
		<Constructor Id=""1"" TypeId=""{268004ae-ef6b-4e9b-8425-127220d84719}"" />
		<Method Id=""3"" ParentId=""1"" Name=""GetSPOTenantWebTemplates"">
			<Parameters>
				<Parameter Type=""UInt32"">0</Parameter>
				<Parameter Type=""Int32"">0</Parameter>
			</Parameters>
		</Method>
	</ObjectPaths>
</Request>";
            var result = ClientSvcHelper.Execute(CurrentContext, payload,true);
            var deserialized = JsonConvert.DeserializeObject<JArray>(result);
            foreach(var template in deserialized[6]["_Child_Items_"].ToObject<List<TenantWebTemplate>>())
            {
                WriteObject(template);
            }
        }
    }
}
