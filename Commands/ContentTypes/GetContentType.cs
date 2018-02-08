using System.Management.Automation;
using System;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Linq;
using System.Collections.Generic;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "ContentType")]
    [CmdletHelp(VerbsCommon.Get, "ContentType", "Retrieves a content type",
        Category = CmdletHelpCategory.ContentTypes,
        OutputType = typeof(ContentType),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.contenttype.aspx")]
    [CmdletExample(
        Code = @"PS:> Get-PnPContentType ",
        Remarks = @"This will get a listing of all available content types within the current web",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPContentType -InSiteHierarchy",
        Remarks = @"This will get a listing of all available content types within the site collection",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Get-PnPContentType -Identity ""Project Document""",
        Remarks = @"This will get the content type with the name ""Project Document"" within the current context",
        SortOrder = 3)]
    [CmdletExample(
        Code = @"PS:> Get-PnPContentType -List ""Documents""",
        Remarks = @"This will get a listing of all available content types within the list ""Documents""",
        SortOrder = 4)]
    public class GetContentType : PnPCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, HelpMessage = "Name or ID of the content type to retrieve")]
        public ContentTypePipeBind Identity;
        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "List to query")]
        public ListPipeBind List;
        [Parameter(Mandatory = false, ValueFromPipeline = false, HelpMessage = "Search site hierarchy for content types")]
        public SwitchParameter InSiteHierarchy;

        protected override void ExecuteCmdlet()
        {

            if (Identity != null)
            {
                if (List == null)
                {
                    ContentType ct = null;
                    if (Identity != null)
                    {
                        ct = Identity.GetContentType(Context, InSiteHierarchy);
                    }
                    if (ct != null)
                    {

                        WriteObject(ct);
                    }
                    else
                    {
                        throw new PSArgumentException($"No ContentType found with the Identity '{(Identity.Id != null ? Identity.Id : Identity.Name)}'", "Identity");

                    }
                }
                else
                {
                    var list = List.GetList(Context);

                    if (!string.IsNullOrEmpty(Identity.Id))
                    {
                        var cts = ExecuteGetRequest<ContentType>(Context, $"Web/Lists(guid'{list.Id}')/ContentTypes('{Identity.Id}')");
                        if (cts != null)
                        {
                            WriteObject(cts, false);
                        }
                        else
                        {
                            WriteError(new ErrorRecord(new ArgumentException(String.Format("Content Type Id: {0} does not exist in the list: {1}", Identity.Id, list.Title)), "CONTENTTYPEDOESNOTEXIST", ErrorCategory.InvalidArgument, this));
                        }
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        var cts = new RestRequest(Context, $"Web/Lists(guid'{list.Id}')/ContentTypes").Select("Name", "Id").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault(c => c.Name == Identity.Name);
                        if (cts != null)
                        {
                            var ct = new RestRequest(Context, $"Web/Lists(guid'{list.Id}')/ContentTypes('{cts.Id.StringValue}')").Get<ContentType>();
                            WriteObject(ct, false);
                        }
                        else
                        {
                            WriteError(new ErrorRecord(new ArgumentException(String.Format("Content Type Name: {0} does not exist in the list: {1}", Identity.Name, list.Title)), "CONTENTTYPEDOESNOTEXIST", ErrorCategory.InvalidArgument, this));
                        }
                    }
                }
            }
            else
            {
                if (List == null)
                {
                    List<ContentType> cts = null;
                    if (InSiteHierarchy)
                    {
                        cts = new RestRequest(Context, "Site/RootWeb/ContentTypes").Get<ResponseCollection<ContentType>>().Items;
                    }
                    else
                    {
                        cts = new RestRequest(Context, $"Web/ContentTypes").Get<ResponseCollection<ContentType>>().Items;
                    }
                    WriteObject(cts, true);
                }
                else
                {
                    var list = List.GetList(Context);
                    var cts = new RestRequest(Context, $"Web/Lists(guid'{list.Id}')/ContentTypes").Get<ResponseCollection<ContentType>>().Items;
                    WriteObject(cts, true);
                }
            }
        }
    }
}

