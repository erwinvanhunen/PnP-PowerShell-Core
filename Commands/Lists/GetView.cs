using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "View")]
    [CmdletHelp(VerbsCommon.Get, "View", "Returns one or all views from a list",
        Category = CmdletHelpCategory.Lists,
        OutputType = typeof(View),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.view.aspx")]
    [CmdletExample(
        Code = @"Get-PnPView -List ""Demo List""",
        Remarks = @"Returns all views associated from the specified list",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"Get-PnPView -List ""Demo List"" -Identity ""Demo View""",
        Remarks = @"Returns the view called ""Demo View"" from the specified list",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"Get-PnPView -List ""Demo List"" -Identity ""5275148a-6c6c-43d8-999a-d2186989a661""",
        Remarks = @"Returns the view with the specified ID from the specified list",
        SortOrder = 3)]
    public class GetView : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID or Url of the list.")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, HelpMessage = "The ID or name of the view")]
        public ViewPipeBind Identity;

        protected override void ExecuteCmdlet()
        {

            var list = List.GetList(Context);
            if (list != null)
            {
                if (Identity != null)
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists/GetById(guid'{list.Id}')/Views(guid'{Identity.Id}')").Get<View>());
                    }
                    else if (!string.IsNullOrEmpty(Identity.Title))
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists/GetById(guid'{list.Id}')/Views/GetByTitle('{Identity.Title}')").Get<View>());
                    }
                }
                else
                {
                    WriteObject(new RestRequest(Context, $"Web/Lists/GetById(guid'{list.Id}')/Views").Get<ResponseCollection<View>>().Items, true);
                }
            }
        }
    }
}
