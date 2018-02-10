using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Fields
{
    [Cmdlet(VerbsCommon.Add, "FieldFromXml")]
    [CmdletHelp(VerbsCommon.Add, "FieldFromXml", "Adds a field to a list or as a site column based upon a CAML/XML field definition",
        Category = CmdletHelpCategory.Fields)]
    [CmdletExample(
        Code = @"PS:> $xml = '<Field Type=""Text"" Name=""PSCmdletTest"" DisplayName=""PSCmdletTest"" ID=""{27d81055-f208-41c9-a976-61c5473eed4a}"" Group=""Test"" Required=""FALSE"" StaticName=""PSCmdletTest"" />'
PS:> Add-PnPFieldFromXml -FieldXml $xml",
        Remarks = "Adds a field with the specified field CAML code to the site.",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> $xml = '<Field Type=""Text"" Name=""PSCmdletTest"" DisplayName=""PSCmdletTest"" ID=""{27d81055-f208-41c9-a976-61c5473eed4a}"" Group=""Test"" Required=""FALSE"" StaticName=""PSCmdletTest"" />'
PS:> Add-PnPFieldFromXml -List ""Demo List"" -FieldXml $xml",
        Remarks = @"Adds a field with the specified field CAML code to the list ""Demo List"".",
        SortOrder = 2)]
    [CmdletRelatedLink(
        Text = "Field CAML",
        Url = "http://msdn.microsoft.com/en-us/library/office/ms437580(v=office.15).aspx")]
    public class AddFieldFromXml : PnPCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The name of the list, its ID or an actual list object where this field needs to be added")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, HelpMessage = "CAML snippet containing the field definition. See http://msdn.microsoft.com/en-us/library/office/ms437580(v=office.15).aspx", Position = 0)]
        public string FieldXml;

        protected override void ExecuteCmdlet()
        {
            Field returnField;
            var postObject = new Dictionary<string, object>();
            postObject.Add("parameters", new XmlSchemaFieldCreationInformation() { SchemaXml = FieldXml });
            var json = JsonConvert.SerializeObject(postObject);
            if (MyInvocation.BoundParameters.ContainsKey("List"))
            {
                var list = List.GetList(Context);
                returnField = new RestRequest(Context, $"{list.ObjectPath}/fields/createfieldasxml").Post<Field>(json);
            }
            else
            {
                returnField = new RestRequest(Context, $"{Context.Web.ObjectPath}/fields/createfieldasxml").Post<Field>(json);
            }
            WriteObject(returnField);
        }

    }

}
