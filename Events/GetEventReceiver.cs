using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get,"EventReceiver")]

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
                var list = List.GetList();

                if (list != null)
                {
                    if (!MyInvocation.BoundParameters.ContainsKey("Identity"))
                    {
                        WriteObject(ExecuteGetRequest<ResponseCollection<EventReceiver>>($"Web/Lists/GetById(guid'{list.Id}')/EventReceivers").Items, true);
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
                    WriteObject(ExecuteGetRequest<ResponseCollection<EventReceiver>>($"Web/EventReceivers").Items, true);

                }
                else
                {
                    WriteObject(Identity.GetEventReceiverOnWeb());
                }
            }
        }
    }
}
