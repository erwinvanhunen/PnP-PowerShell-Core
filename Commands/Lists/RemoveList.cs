using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "List", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Remove,"List","Deletes a list",
        Category = CmdletHelpCategory.Lists)]
    [CmdletExample(
        Code = "PS:> Remove-PnPList -Identity Announcements",
        SortOrder = 1,
        Remarks = @"Removes the list named 'Announcements'. Asks for confirmation.")]
    [CmdletExample(
        Code = "PS:> Remove-PnPList -Identity Announcements -Force",
        SortOrder = 2,
        Remarks = @"Removes the list named 'Announcements' without asking for confirmation.")]
    [CmdletExample(
        Code = "PS:> Remove-PnPList -Title Announcements -Recycle",
        SortOrder = 3,
        Remarks = @"Removes the list named 'Announcements' and saves to the Recycle Bin")]
    public class RemoveList : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID or Title of the list.")]
        public ListPipeBind Identity = new ListPipeBind();

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.")]
        public SwitchParameter Force;
        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var list = Identity.GetList(Context);
                if (list != null)
                {
                    if (Force || ShouldContinue($"Remove List '{list.Title}'", "Confirm"))
                    {
                        if (Recycle)
                        {
                            new RestRequest(Context, $"{list.ObjectPath}/recycle").Post();
                        }
                        else
                        {
                            new RestRequest(Context, list.ObjectPath).Delete();
                        }
                    }
                }
            }
        }
    }

}
