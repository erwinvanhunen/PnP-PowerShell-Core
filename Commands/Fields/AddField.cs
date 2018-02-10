using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Fields
{
    [Cmdlet(VerbsCommon.Add, "Field", DefaultParameterSetName = "Add field to list")]
    [CmdletHelp(VerbsCommon.Add, "Field", "Add a field",
        "Adds a field to a list or as a site column",
        Category = CmdletHelpCategory.Fields)]
    [CmdletExample(
     Code = @"PS:> Add-PnPField -List ""Demo list"" -DisplayName ""Location"" -InternalName ""SPSLocation"" -Type Choice -Group ""Demo Group"" -AddToDefaultView -Choices ""Stockholm"",""Helsinki"",""Oslo""",
     Remarks = @"This will add a field of type Choice to the list ""Demo List"".", SortOrder = 1)]
    [CmdletExample(
     Code = @"PS:>Add-PnPField -List ""Demo list"" -DisplayName ""Speakers"" -InternalName ""SPSSpeakers"" -Type MultiChoice -Group ""Demo Group"" -AddToDefaultView -Choices ""Obiwan Kenobi"",""Darth Vader"", ""Anakin Skywalker""",
Remarks = @"This will add a field of type Multiple Choice to the list ""Demo List"". (you can pick several choices for the same item)", SortOrder = 2)]
    [CmdletAdditionalParameter(ParameterType = typeof(string[]), ParameterName = "Choices", HelpMessage = "Specify choices, only valid if the field type is Choice", ParameterSetName = "Add field to list")]
    [CmdletAdditionalParameter(ParameterType = typeof(string[]), ParameterName = "Choices", HelpMessage = "Specify choices, only valid if the field type is Choice", ParameterSetName = "Add field to Web")]
    public class AddField : PnPCmdlet, IDynamicParameters
    {
        private const string ParameterSet_FIELDREFLIST = "Add field reference to list";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "Add field to list", HelpMessage = "The name of the list, its ID or an actual list object where this field needs to be added")]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_FIELDREFLIST, HelpMessage = "The name of the list, its ID or an actual list object where this field needs to be added")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FIELDREFLIST, HelpMessage = "The name of the field, its ID or an actual field object that needs to be added")]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true, ParameterSetName = "Add field to list", HelpMessage = "The display name of the field")]
        [Parameter(Mandatory = true, ParameterSetName = "Add field to Web", HelpMessage = "The display name of the field")]
        public string DisplayName;

        [Parameter(Mandatory = true, ParameterSetName = "Add field to list", HelpMessage = "The internal name of the field")]
        [Parameter(Mandatory = true, ParameterSetName = "Add field to Web", HelpMessage = "The internal name of the field")]
        public string InternalName;

        [Parameter(Mandatory = true, ParameterSetName = "Add field to list", HelpMessage = "The type of the field like Choice, Note, MultiChoice")]
        [Parameter(Mandatory = true, ParameterSetName = "Add field to Web", HelpMessage = "The type of the field like Choice, Note, MultiChoice")]
        public FieldType Type;

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "The ID of the field, must be unique")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field to Web", HelpMessage = "The ID of the field, must be unique")]
        public GuidPipeBind Id = new GuidPipeBind();

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "Switch Parameter if this field must be added to the default view")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field by XML to list", HelpMessage = "Switch Parameter if this field must be added to the default view")]
        public SwitchParameter AddToDefaultView;

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "Switch Parameter if the field is a required field")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field by XML to list", HelpMessage = "Switch Parameter if the field is a required field")]
        public SwitchParameter Required;

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "The group name to where this field belongs to")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field by XML to list", HelpMessage = "The group name to where this field belongs to")]
        public string Group;

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "The Client Side Component Id to set to the field")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field to Web", HelpMessage = "The Client Side Component Id to set to the field")]
        public GuidPipeBind ClientSideComponentId;

        [Parameter(Mandatory = false, ParameterSetName = "Add field to list", HelpMessage = "The Client Side Component Properties to set to the field")]
        [Parameter(Mandatory = false, ParameterSetName = "Add field to Web", HelpMessage = "The Client Side Component Properties to set to the field")]
        public string ClientSideComponentProperties;

        public object GetDynamicParameters()
        {
            if (Type == FieldType.Choice || Type == FieldType.MultiChoice)
            {
                _context = new ChoiceFieldDynamicParameters();
                return _context;
            }
            return null;
        }

        private ChoiceFieldDynamicParameters _context;

        protected override void ExecuteCmdlet()
        {

            if (Id.Id == Guid.Empty)
            {
                Id = new GuidPipeBind(Guid.NewGuid());
            }

            if (List != null)
            {
                var list = List.GetList(Context);
                Field f;
                if (ParameterSetName != ParameterSet_FIELDREFLIST)
                {
                    string json = GetFieldJson();

                    var returnField = new RestRequest(Context, $"{list.ObjectPath}/fields").Post<Field>(json);
                    WriteObject(returnField);

                }
                else
                {
                    // Site column

                    Field field = Field.GetWebField(Context);
                    if (field == null)
                    {
                        field = Field.GetSiteField(Context);
                    }
                    if (field != null)
                    {
                        var postObject = new Dictionary<string, object>();
                        postObject.Add("parameters", new XmlSchemaFieldCreationInformation() { SchemaXml = field.SchemaXml });
                        var json = JsonConvert.SerializeObject(postObject);
                        var returnField = new RestRequest(Context, $"{list.ObjectPath}/fields/createfieldasxml").Post<Field>(json);
                        WriteObject(returnField);
                    }
                }
            }
            else
            {
                var json = GetFieldJson();
                var returnField = new RestRequest(Context, $"{Context.Web.ObjectPath}/fields").Post<Field>(json);
                WriteObject(returnField);
            }
        }

        private string GetFieldJson()
        {
            var field = new Dictionary<string, object>();
            field.Add("Id", Id.Id);
            field.Add("InternalName", InternalName);
            field.Add("Title", DisplayName);
            if (!string.IsNullOrEmpty(Group))
            {
                field.Add("Group", Group);
            }
            field.Add("Required", Required.IsPresent);
            //field.Add("AddToDefaultView", AddToDefaultView);

            if (ClientSideComponentId != null)
            {
                field.Add("ClientSideComponentId", ClientSideComponentId);
            }
            if (!string.IsNullOrEmpty(ClientSideComponentProperties))
            {
                field.Add("ClientSideComponentProperties", ClientSideComponentProperties);
            }
            var fieldType = "SP.Field";
            if (Type == FieldType.Choice || Type == FieldType.MultiChoice)
            {
                fieldType = Type == FieldType.Choice ? "SP.FieldChoice" : "SP.FieldMultiChoice";
                field.Add("Choices", new FieldChoices() { Results = _context.Choices });
            }

            field.Add("__metadata", new Dictionary<string, string>() { { "type", fieldType } });
            field.Add("FieldTypeKind", (int)Type);
            var json = JsonConvert.SerializeObject(field);
            return json;
        }

        public class ChoiceFieldDynamicParameters
        {
            [Parameter(Mandatory = false)]
            public string[] Choices
            {
                get { return _choices; }
                set { _choices = value; }
            }
            private string[] _choices;
        }

    }

}
