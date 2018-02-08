using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;


namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "ContentTypeToList")]
    [CmdletHelp(VerbsCommon.Add,"ContentTypeToList","Adds a new content type to a list",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Add-PnPContentTypeToList -List ""Documents"" -ContentType ""Project Document"" -DefaultContentType",
        Remarks = @"This will add an existing content type to a list and sets it as the default content type",
        SortOrder = 1)]
    public class AddContentTypeToList : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Specifies the list the content type needs to be added to")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, HelpMessage = "Specifies the content type that needs to be added to the list")]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false, HelpMessage = "Specify if the content type needs to be the default content type or not")]
        public SwitchParameter DefaultContentType;

        protected override void ExecuteCmdlet()
        {
            ContentType ct = ContentType.GetContentType(CurrentContext, true);
            List list = List.GetList(CurrentContext);
            // make sure that the list supports contenttypes;
            list.ContentTypesEnabled = true;
            list.Update();
            if (ct != null)
            {
                new RestRequest(CurrentContext, $"Web/Lists(guid'{list.Id}')/ContentTypes").Post(ct);
            }
        }

    }
}
