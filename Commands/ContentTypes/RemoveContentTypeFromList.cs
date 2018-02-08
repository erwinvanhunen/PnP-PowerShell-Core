using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{

    [Cmdlet(VerbsCommon.Remove, "ContentTypeFromList")]
    [CmdletHelp(VerbsCommon.Remove, "ContentTypeFromList", "Removes a content type from a list",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPContentTypeFromList -List ""Documents"" -ContentType ""Project Document""",
        Remarks = @"This will remove a content type called ""Project Document"" from the ""Documents"" list",
        SortOrder = 1)]
    public class RemoveContentTypeFromList : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The name of the list, its ID or an actual list object from where the content type needs to be removed from")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, HelpMessage = "The name of a content type, its ID or an actual content type object that needs to be removed from the specified list.")]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(Context);

            var ct = ContentType.GetContentType(Context, true);
            if (ct == null)
            {
                ct = ContentType.GetContentTypeFromList(Context, list);
            }

            var ctToRemove = list.ContentTypes.FirstOrDefault(c => c.StringId == ct.StringId);
            if (ctToRemove != null)
            {
                new RestRequest(Context, $"Web/Lists(guid'{list.Id}')/ContentTypes('{ctToRemove.StringId}')").Delete();
            }
        }

    }
}
