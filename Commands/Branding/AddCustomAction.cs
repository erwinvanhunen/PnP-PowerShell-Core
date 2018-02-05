﻿using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Add, "CustomAction")]
    [CmdletHelp(VerbsCommon.Add, "CustomAction", "Adds a custom action",
        "Adds a user custom action to a web or sitecollection.",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(Code = @"$cUIExtn = ""<CommandUIExtension><CommandUIDefinitions><CommandUIDefinition Location=""""Ribbon.List.Share.Controls._children""""><Button Id=""""Ribbon.List.Share.GetItemsCountButton"""" Alt=""""Get list items count"""" Sequence=""""11"""" Command=""""Invoke_GetItemsCountButtonRequest"""" LabelText=""""Get Items Count"""" TemplateAlias=""""o1"""" Image32by32=""""_layouts/15/images/placeholder32x32.png"""" Image16by16=""""_layouts/15/images/placeholder16x16.png"""" /></CommandUIDefinition></CommandUIDefinitions><CommandUIHandlers><CommandUIHandler Command=""""Invoke_GetItemsCountButtonRequest"""" CommandAction=""""javascript: alert('Total items in this list: '+ ctx.TotalListItems);"""" EnabledScript=""""javascript: function checkEnable() { return (true);} checkEnable();""""/></CommandUIHandlers></CommandUIExtension>""

Add-PnPCustomAction -Name 'GetItemsCount' -Title 'Invoke GetItemsCount Action' -Description 'Adds custom action to custom list ribbon' -Group 'SiteActions' -Location 'CommandUI.Ribbon' -CommandUIExtension $cUIExtn",
    Remarks = @"Adds a new custom action to the custom list template, and sets the Title, Name and other fields with the specified values. On click it shows the number of items in that list. Notice: escape quotes in CommandUIExtension.",
    SortOrder = 1)]
    [CmdletRelatedLink(
        Text = "UserCustomAction",
        Url = "https://msdn.microsoft.com/en-us/library/office/microsoft.sharepoint.client.usercustomaction.aspx")]
    [CmdletRelatedLink(
        Text = "BasePermissions",
        Url = "https://msdn.microsoft.com/en-us/library/office/microsoft.sharepoint.client.basepermissions.aspx")]
    public class AddCustomAction : PnPCmdlet
    {
        private const string ParameterSet_DEFAULT = "Default";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";
        [Parameter(Mandatory = true, HelpMessage = "The name of the custom action", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = true, HelpMessage = "The name of the custom action", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public string Name = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "The title of the custom action", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = true, HelpMessage = "The title of the custom action", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public string Title = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "The description of the custom action", ParameterSetName = ParameterSet_DEFAULT)]
        public string Description = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "The group where this custom action needs to be added like 'SiteActions'", ParameterSetName = ParameterSet_DEFAULT)]
        public string Group = string.Empty;

        [Parameter(Mandatory = true, HelpMessage = "The actual location where this custom action need to be added like 'CommandUI.Ribbon'", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = true, HelpMessage = "The actual location where this custom action need to be added like 'CommandUI.Ribbon'", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public string Location = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "Sequence of this CustomAction being injected. Use when you have a specific sequence with which to have multiple CustomActions being added to the page.", ParameterSetName = ParameterSet_DEFAULT)]
        public int Sequence = 0;

        [Parameter(Mandatory = false, HelpMessage = "The URL, URI or ECMAScript (JScript, JavaScript) function associated with the action", ParameterSetName = ParameterSet_DEFAULT)]
        public string Url = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "The URL of the image associated with the custom action", ParameterSetName = ParameterSet_DEFAULT)]
        public string ImageUrl = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "XML fragment that determines user interface properties of the custom action", ParameterSetName = ParameterSet_DEFAULT)]
        public string CommandUIExtension = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "The identifier of the object associated with the custom action.", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = false, HelpMessage = "The identifier of the object associated with the custom action.", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public string RegistrationId = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "A string array that contain the permissions needed for the custom action", ParameterSetName = ParameterSet_DEFAULT)]
        public PermissionKind[] Rights;

        [Parameter(Mandatory = false, HelpMessage = "Specifies the type of object associated with the custom action", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = false, HelpMessage = "Specifies the type of object associated with the custom action", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public UserCustomActionRegistrationType RegistrationType;

        [Parameter(Mandatory = false, HelpMessage = "The scope of the CustomAction to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.", ParameterSetName = ParameterSet_DEFAULT)]
#if !ONPREMISES
        [Parameter(Mandatory = false, HelpMessage = "The scope of the CustomAction to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
#endif
        public CustomActionScope Scope = CustomActionScope.Web;
#if !ONPREMISES
        [Parameter(Mandatory = true, HelpMessage = "The Client Side Component Id of the custom action", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public GuidPipeBind ClientSideComponentId;

        [Parameter(Mandatory = false, HelpMessage = "The Client Side Component Properties of the custom action. Specify values as a json string : \"{Property1 : 'Value1', Property2: 'Value2'}\"", ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public string ClientSideComponentProperties;
#endif
        protected override void ExecuteCmdlet()
        {
            var permissions = new BasePermissions();
            if (Rights != null)
            {
                foreach (var kind in Rights)
                {
                    permissions.Set(kind);
                }
            }
            var dict = new Dictionary<string, object>();

            //CustomActionEntity ca = null;
            if (ParameterSetName == ParameterSet_DEFAULT)
            {
                dict = new Dictionary<string, object>(){
                    { "Name",Name },
                    { "ImageUrl", ImageUrl },
                    { "CommandUIExtension", CommandUIExtension },
                    { "RegistrationId",RegistrationId },
                    { "RegistrationType",RegistrationType },
                    { "Description",Description },
                    { "Location",Location},
                    { "Group",Group },
                    { "Sequence", Sequence },
                    { "Title",Title },
                    { "Url",Url }
                };
            }
            else
            {
                dict = new Dictionary<string, object>()
                {
                    {"Name",Name },
                    {"Title",Title },
                    {"Location",Location },
                    {"ClientSideComponentId", ClientSideComponentId.Id },
                    {"ClientSideComponentProperties", ClientSideComponentProperties }
                };
                

                if (MyInvocation.BoundParameters.ContainsKey("RegistrationId"))
                {
                    dict.Add("RegistrationId", RegistrationId);
                }

                if (MyInvocation.BoundParameters.ContainsKey("RegistrationType"))
                {
                    dict.Add("RegistrationType", RegistrationType);
                }
            }

            switch (Scope)
            {
                case CustomActionScope.Web:
                    new RestRequest("Web/UserCustomActions").Post(new MetadataType("SP.UserCustomAction"), dict);
                    break;

                case CustomActionScope.Site:
                    new RestRequest("Site/UserCustomActions").Post(new MetadataType("SP.UserCustomAction"), dict);
                    break;

                case CustomActionScope.All:
                    WriteWarning("CustomActionScope 'All' is not supported for adding CustomActions");
                    break;
            }
        }
    }
}
