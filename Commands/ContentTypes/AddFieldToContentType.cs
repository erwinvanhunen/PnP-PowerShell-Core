using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "FieldToContentType")]
    [CmdletHelp(VerbsCommon.Add, "FieldToContentType", "Adds an existing site column to a content type",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Add-PnPFieldToContentType -Field ""Project_Name"" -ContentType ""Project Document""",
        Remarks = @"This will add an existing site column with an internal name of ""Project_Name"" to a content type called ""Project Document""",
        SortOrder = 1)]
    public class AddFieldToContentType : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Specifies the field that needs to be added to the content type")]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true, HelpMessage = "Specifies which content type a field needs to be added to")]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false, HelpMessage = "Specifies whether the field is required or not")]
        public SwitchParameter Required;

        [Parameter(Mandatory = false, HelpMessage = "Specifies whether the field should be hidden or not")]
        public SwitchParameter Hidden;

        protected override void ExecuteCmdlet()
        {
            Field field = Field.Field;
            if (field == null)
            {
                if (Field.Id != Guid.Empty)
                {
                    field = new RestRequest(CurrentContext, $"Web/Fields(guid'{Field.Id}')").Get<Field>();
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    field = new RestRequest(CurrentContext, $"Web/Fields/GetByInternalNameOrTitle('{Field.Name}')").Get<Field>();
                }
            }
            if (field != null)
            {
                var ct = ContentType.GetContentType(CurrentContext, true);
                var fieldLink = new FieldLink()
                {
                 //   FieldInternalName = field.InternalName,
                    Hidden = Hidden,
                    Name = field.Title,
                    Required = Required,
                    Id = field.Id
                };
                new RestRequest(CurrentContext, $"Web/AvailableContentTypes('{ct.StringId}')/FieldLinks").Post(fieldLink);
            }
            else
            {
                throw new Exception("Field not found");
            }
        }


    }
}
