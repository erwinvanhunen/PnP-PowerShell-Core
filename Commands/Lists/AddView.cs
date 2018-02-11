using System.Collections.Generic;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "View")]
    [CmdletHelp(VerbsCommon.Add,"View","Adds a view to a list")]
    [CmdletExample(
        Code = @"Add-PnPView -List ""Demo List"" -Title ""Demo View"" -Fields ""Title"",""Address""",
        Remarks = @"Adds a view named ""Demo view"" to the ""Demo List"" list.",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"Add-PnPView -List ""Demo List"" -Title ""Demo View"" -Fields ""Title"",""Address"" -Paged",
        Remarks = @"Adds a view named ""Demo view"" to the ""Demo List"" list and makes sure there's paging on this view.",
        SortOrder = 2)]
    public class AddView : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID or Url of the list.")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, HelpMessage = "The title of the view.")]
        public string Title;

        [Parameter(Mandatory = false, HelpMessage = "A valid CAML Query.")]
        public string Query;

        [Parameter(Mandatory = true, HelpMessage = "A list of fields to add.")]
        public string[] Fields;

        [Parameter(Mandatory = false, HelpMessage = "The type of view to add.")]
        public ViewType ViewType = ViewType.None;

        [Parameter(Mandatory = false, HelpMessage = "The row limit for the view. Defaults to 30.")]
        public uint RowLimit = 30;

        [Parameter(Mandatory = false, HelpMessage = "If specified, a personal view will be created.")]
        public SwitchParameter Personal;

        [Parameter(Mandatory = false, HelpMessage = "If specified, the view will be set as the default view for the list.")]
        public SwitchParameter SetAsDefault;

        [Parameter(Mandatory = false, HelpMessage = "If specified, the view will have paging.")]
        public SwitchParameter Paged;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(Context);
            if (list != null)
            {
                var view = new View();
                view.Title = Title;
                view.ViewType = ViewType;
                //view.ViewFields = new List<string>(Fields);
                view.DefaultView = SetAsDefault;
                if(MyInvocation.BoundParameters.ContainsKey("Query"))
                {
                    view.ViewQuery = Query;
                }
                view.PersonalView = Personal;
                view.Paged = Paged;
                view.RowLimit = (int)RowLimit;
                var newView = new RestRequest(Context, $"{list.ObjectPath}/Views").Post<View>(view);
                foreach(var viewField in Fields)
                {
                    new RestRequest(Context, $"{newView.ObjectPath}/viewfields/addviewfield('{viewField}')").Post();
                }
                newView = new RestRequest(Context, newView.ObjectPath).Expand("ViewFields").Get<View>();
                WriteObject(newView);
            }
        }
    }
}
