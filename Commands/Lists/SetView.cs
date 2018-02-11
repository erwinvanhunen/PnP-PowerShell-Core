using System;
using System.Management.Automation;
using System.Collections;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using System.Collections.Generic;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Set, "View")]
    [CmdletHelp(VerbsCommon.Set, "View", "Change view properties",
        DetailedDescription = "Sets one or more properties of an existing view.",
        Category = CmdletHelpCategory.Fields,
        OutputType = typeof(Field),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.view.aspx")]
    [CmdletExample(
        Code = @"PS:> Set-PnPView -List ""Tasks"" -Identity ""All Tasks"" -Values @{JSLink=""hierarchytaskslist.js|customrendering.js"";Title=""My view""}",
        Remarks = @"Updates the ""All Tasks"" view on list ""Tasks"" to use hierarchytaskslist.js and customrendering.js for the JSLink and changes the title of the view to ""My view""",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPList -Identity ""Tasks"" | Get-PnPView | Set-PnPView -Values @{JSLink=""hierarchytaskslist.js|customrendering.js""}",
        Remarks = @"Updates all views on list ""Tasks"" to use hierarchytaskslist.js and customrendering.js for the JSLink",
        SortOrder = 2)]
    public class SetView : PnPCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, HelpMessage = "The Id, Title or Url of the list")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The Id, Title or instance of the view")]
        public ViewPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "The title of the view.")]
        public string Title;

        [Parameter(Mandatory = false, HelpMessage = "A valid CAML Query.")]
        public string Query;

        [Parameter(Mandatory = false, HelpMessage = "A list of fields to add.")]
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


        [Parameter(Mandatory = false, HelpMessage = "Hashtable of additional properties to update on the view. Use the syntax @{property1=\"value\";property2=\"value\"}.")]
        public Hashtable Values;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(Context);
                if (list == null)
                {
                    throw new PSArgumentException("List provided in the List argument could not be found", "List");
                }
                var view = Identity.GetView(Context, list);

                Dictionary<string, object> viewDict = new Dictionary<string, object>();
                if (MyInvocation.BoundParameters.ContainsKey("Title"))
                {
                    viewDict.Add("Title", Title);
                }
                if (MyInvocation.BoundParameters.ContainsKey("Query"))
                {
                    viewDict.Add("Query", Query);
                }
                if (MyInvocation.BoundParameters.ContainsKey("ViewType"))
                {
                    viewDict.Add("ViewType", ViewType.ToString());
                }
                if (MyInvocation.BoundParameters.ContainsKey("RowLimit"))
                {
                    viewDict.Add("RowLimit", RowLimit);
                }
                if (MyInvocation.BoundParameters.ContainsKey("SetAsDefault"))
                {
                    viewDict.Add("DefaultView", SetAsDefault.ToBool());
                }
                if (MyInvocation.BoundParameters.ContainsKey("Paged"))
                {
                    viewDict.Add("Paged", Paged.ToBool());
                }
                if (Values != null)
                {
                    foreach (string key in Values.Keys)
                    {
                        var value = Values[key];

                        viewDict.Add(key, value);
                    }
                }
                WriteObject(new RestRequest(Context, view.ObjectPath).Merge<View>(new MetadataType("SP.View"), viewDict));
                if (Fields != null)
                {
                    var batch = new BatchRequest(Context);
                    // clear all fields from view
                    //batch.Post($"{view.ObjectPath}/ViewFields/RemoveAllViewFields");
                    new RestRequest(Context, $"{view.ObjectPath}/ViewFields/RemoveAllViewFields").Post();
                    foreach (var viewField in Fields)
                    {
                        //    batch.Post($"{view.ObjectPath}/viewfields/addviewfield('{viewField}')");

                        new RestRequest(Context, $"{view.ObjectPath}/viewfields/addviewfield('{viewField}')").Post();

                    }
                    batch.Execute();
                }
            }
        }
    }
}