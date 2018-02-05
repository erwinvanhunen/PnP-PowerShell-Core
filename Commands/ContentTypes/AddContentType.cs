using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Mime;

namespace SharePointPnP.PowerShell.Core.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "ContentType")]
    [CmdletHelp(VerbsCommon.Add,"ContentType","Adds a new content type",
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
        public Model.ContentType ParentContentType;

        protected override void ExecuteCmdlet()
        {
            var dict = new Dictionary<string, object>()
            {
                {"Name",Name }
            };
            if(MyInvocation.BoundParameters.ContainsKey("ContentTypeId"))
            {
                dict.Add("ContentTypeId", new ContentTypeId() { StringValue = ContentTypeId });
            }
            if(MyInvocation.BoundParameters.ContainsKey("Description"))
            {
                dict.Add("Description", Description);
            }
            if (MyInvocation.BoundParameters.ContainsKey("Group"))
            {
                dict.Add("Group", Group);
            }
            WriteObject(JsonConvert.SerializeObject(dict));
            //var ct = SelectedWeb.CreateContentType(Name, Description, ContentTypeId, Group, ParentContentType);
            //ClientContext.Load(ct);
            //ClientContext.ExecuteQueryRetry();
            //WriteObject(ct);
        }


    }
}
