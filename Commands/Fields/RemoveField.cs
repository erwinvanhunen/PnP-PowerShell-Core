using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Fields
{
    [Cmdlet(VerbsCommon.Remove, "Field", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Remove,"Field","Removes a field from a list or a site",
        Category = CmdletHelpCategory.Fields)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPField -Identity ""Speakers""",
        Remarks = @"Removes the speakers field from the site columns",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPField -List ""Demo list"" -Identity ""Speakers""",
        Remarks = @"Removes the speakers field from the list Demo list",
        SortOrder = 1)]
    public class RemoveField : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The field object or name of field to remove")]
        public FieldPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 1, HelpMessage = "The list object or name where to remove the field from")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.BoundParameters.ContainsKey("List"))
            {
                var list = List.GetList(Context);

                var f = Identity.GetListField(Context, list);

                if (Force || ShouldContinue($"Delete field {f.InternalName}?", "Confirm"))
                {
                    new RestRequest(Context, f.ObjectPath).Delete();
                }
            }
            else
            {
                var f = Identity.GetWebField(Context);
                if (Force || ShouldContinue($"Delete field {f.InternalName}?", "Confirm"))
                {
                    new RestRequest(Context, f.ObjectPath).Delete();
                }
            }
        }
    }
}
