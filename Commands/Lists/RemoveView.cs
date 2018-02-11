using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "View", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Remove, "View", "Deletes a view from a list",
        Category = CmdletHelpCategory.Lists)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPView -List ""Demo List"" -Identity ""All Items""",
        SortOrder = 1,
        Remarks = @"Removes the view with title ""All Items"" from the ""Demo List"" list.")]
    public class RemoveView : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID or Title of the view.")]
        public ViewPipeBind Identity = new ViewPipeBind();

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, HelpMessage = "The ID or Url of the list.")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(Context);

                if (list != null)
                {
                    View view = Identity.GetView(Context, list);

                    if (view != null)
                    {
                        if (Force || ShouldContinue($"Remove view '{view.Title}'?", "Confirm"))
                        {
                            new RestRequest(Context, view.ObjectPath).Delete();
                        }
                    }
                }
            }
        }
    }

}
