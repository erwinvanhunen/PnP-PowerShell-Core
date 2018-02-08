using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Mime;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "ContentType")]
    [CmdletHelp(VerbsCommon.Add, "ContentType", "Adds a new content type",
        Category = CmdletHelpCategory.ContentTypes)]
    [CmdletExample(
        Code = @"PS:> Add-PnPContentType -Name ""Project Document"" -Description ""Use for Contoso projects"" -Group ""Contoso Content Types"" -ParentContentType $ct",
        Remarks = @"This will add a new content type based on the parent content type stored in the $ct variable.",
        SortOrder = 1)]
    public class AddContentType : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "Specify the name of the new content type")]
        public string Name;

        [Parameter(Mandatory = false, HelpMessage = "If specified, in the format of 0x0100233af432334r434343f32f3, will create a content type with the specific ID")]
        public string ContentTypeId;

        [Parameter(Mandatory = false, HelpMessage = "Specifies the description of the new content type")]
        public string Description;

        [Parameter(Mandatory = false, HelpMessage = "Specifies the group of the new content type")]
        public string Group;

        [Parameter(Mandatory = false, HelpMessage = "Specifies the parent of the new content type")]
        public ContentTypePipeBind ParentContentType;

        protected override void ExecuteCmdlet()
        {
            Model.ContentType ct = new Model.ContentType();
            ct.Name = Name;
            
            if (MyInvocation.BoundParameters.ContainsKey("ContentTypeId"))
            {
                ct.Id = new Model.ContentTypeId() { StringValue = ContentTypeId };
            }
            if (MyInvocation.BoundParameters.ContainsKey("Description"))
            {
                ct.Description = Description;
            }
            if (MyInvocation.BoundParameters.ContainsKey("Group"))
            {
                ct.Group = Group;
            }          
            if(MyInvocation.BoundParameters.ContainsKey("ParentContentType"))
            {
                var parentCt = ParentContentType.GetContentType(CurrentContext, true);
                if(parentCt != null)
                {
                    ct.Parent = parentCt;
                }
            }
            new RestRequest(CurrentContext, "Web/ContentTypes").Post(ct);
          
        }


    }
}
