using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "FieldFromContentType")]
    [CmdletHelp(VerbsCommon.Remove, "FieldFromContentType", "Removes a site column from a content type",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
     Code = @"PS:> Remove-PnPFieldFromContentType -Field ""Project_Name"" -ContentType ""Project Document""",
     Remarks = @"This will remove the site column with an internal name of ""Project_Name"" from a content type called ""Project Document""", SortOrder = 1)]
    [CmdletExample(
     Code = @"PS:> Remove-PnPFieldFromContentType -Field ""Project_Name"" -ContentType ""Project Document"" -DoNotUpdateChildren",
     Remarks = @"This will remove the site column with an internal name of ""Project_Name"" from a content type called ""Project Document"". It will not update content types that inherit from the ""Project Document"" content type.", SortOrder = 1)]
    public class RemoveFieldFromContentType : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The field to remove.")]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true, HelpMessage = "The content type where the field is to be removed from.")]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false, HelpMessage = "If specified, inherited content types will not be updated.")]
        public SwitchParameter DoNotUpdateChildren;

        protected override void ExecuteCmdlet()
        {
            Field field = Field.GetSiteField(Context);
            if (field != null)
            {
                var ct = ContentType.GetContentType(Context, true);
                var fieldLink = ct.FieldLinks.FirstOrDefault(fl => fl.Id == field.Id);
                if(fieldLink != null)
                {
                    new RestRequest(Context, fieldLink.ObjectPath).Delete();
                }
            }
            else
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Cannot find field reference in content type"), "FieldRefNotFound", ErrorCategory.ObjectNotFound, ContentType));
            }
        }
    }
}