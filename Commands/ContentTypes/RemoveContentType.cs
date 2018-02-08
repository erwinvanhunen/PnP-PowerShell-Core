using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using System.Net.Mime;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "ContentType")]
    [CmdletHelp(VerbsCommon.Remove,"ContentType","Removes a content type from a web",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPContentType -Identity ""Project Document""",
        Remarks = @"This will remove a content type called ""Project Document"" from the current web",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPContentType -Identity ""Project Document"" -Force",
        Remarks = @"This will remove a content type called ""Project Document"" from the current web with force",
        SortOrder = 2)]
    public class RemoveContentType : PnPCmdlet
    {

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "The name or ID of the content type to remove")]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("Remove content type?", "Confirm"))
            {
                Model.ContentType ct = Identity.GetContentType(CurrentContext,true);
                if (ct != null)
                {
                    new RestRequest(CurrentContext, $"Web/ContentTypes('{ct.StringId}')").Delete();
                }

            }
        }
    }
}
