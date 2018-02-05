using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "EventReceiver")]
    [CmdletHelp(VerbsCommon.Get, "EventReceiver", "Return registered eventreceivers",
        Category = CmdletHelpCategory.EventReceivers)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver",
      Remarks = @"This will return all registered event receivers on the current web", SortOrder = 1)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22",
      Remarks = @"This will return the event receiver with the provided ReceiverId ""fb689d0e-eb99-4f13-beb3-86692fd39f22"" from the current web", SortOrder = 2)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver -Identity MyReceiver",
      Remarks = @"This will return the event receiver with the provided ReceiverName ""MyReceiver"" from the current web", SortOrder = 3)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver -List ""ProjectList""",
      Remarks = @"This will return all registered event receivers in the provided ""ProjectList"" list", SortOrder = 4)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver -List ""ProjectList"" -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22",
      Remarks = @"This will return the event receiver in the provided ""ProjectList"" list with with the provided ReceiverId ""fb689d0e-eb99-4f13-beb3-86692fd39f22""", SortOrder = 5)]
    [CmdletExample(
      Code = @"PS:> Get-PnPEventReceiver -List ""ProjectList"" -Identity MyReceiver",
      Remarks = @"This will return the event receiver in the ""ProjectList"" list with the provided ReceiverName ""MyReceiver""", SortOrder = 5)]
    public class GetEventReceiver : PnPCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = "List", HelpMessage = "The list object from which to get the event receiver object")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The Guid of the event receiver")]
        public EventReceiverPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "List")
            {
                var list = List.GetList(Context);

                if (list != null)
                {
                    if (!MyInvocation.BoundParameters.ContainsKey("Identity"))
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists/GetById(guid'{list.Id}')/EventReceivers").Get<ResponseCollection<EventReceiver>>().Items, true);
                    }
                    else
                    {
                        WriteObject(Identity.GetEventReceiverOnList(list));
                    }
                }
            }
            else
            {
                if (!MyInvocation.BoundParameters.ContainsKey("Identity"))
                {
                    WriteObject(new RestRequest(Context,$"Web/EventReceivers").Get<ResponseCollection<EventReceiver>>().Items, true);

                }
                else
                {
                    WriteObject(Identity.GetEventReceiverOnWeb(Context));
                }
            }
        }
    }
}
