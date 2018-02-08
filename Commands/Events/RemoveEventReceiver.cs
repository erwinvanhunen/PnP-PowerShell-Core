using System.Management.Automation;
using System.Collections.Generic;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Events
{
    [Cmdlet(VerbsCommon.Remove, "EventReceiver", SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Remove,"EventReceiver","Remove an eventreceiver",
        "Removes/unregisters a specific eventreceiver",
                Category = CmdletHelpCategory.EventReceivers)]
    [CmdletExample(Code = @"PS:> Remove-PnPEventReceiver -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22",
                   Remarks = @"This will remove the event receiver with ReceiverId ""fb689d0e-eb99-4f13-beb3-86692fd39f22"" from the current web",
                   SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Remove-PnPEventReceiver -List ProjectList -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22",
                   Remarks = @"This will remove the event receiver with ReceiverId ""fb689d0e-eb99-4f13-beb3-86692fd39f22"" from the ""ProjectList"" list",
                   SortOrder = 2)]
    [CmdletExample(Code = @"PS:> Remove-PnPEventReceiver -List ProjectList -Identity MyReceiver",
                   Remarks = @"This will remove the event receiver with ReceiverName ""MyReceiver"" from the ""ProjectList"" list",
                   SortOrder = 3)]
    [CmdletExample(Code = @"PS:> Remove-PnPEventReceiver -List ProjectList",
                   Remarks = @"This will remove all event receivers from the ""ProjectList"" list",
                   SortOrder = 4)]
    [CmdletExample(Code = @"PS:> Remove-PnPEventReceiver",
                   Remarks = @"This will remove all event receivers from the current site",
                   SortOrder = 5)]
    [CmdletExample(Code = @"PS:> Get-PnPEventReceiver | ? ReceiverUrl -Like ""*azurewebsites.net*"" | Remove-PnPEventReceiver",
                   Remarks = @"This will remove all event receivers from the current site which are pointing to a service hosted on Azure Websites",
                   SortOrder = 6)]
    public class RemoveEventReceiver : PnPCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The Guid of the event receiver on the list")]
        public EventReceiverPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = "List", HelpMessage = "The list object from where to remove the event receiver object")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // Keep a list with all event receivers to remove for better performance and to avoid the collection changing when removing an item in the collection
            var eventReceiversToDelete = new List<EventReceiverDefinition>();

            if (ParameterSetName == "List")
            {
                var list = List.GetList(Context);

                if (MyInvocation.BoundParameters.ContainsKey("Identity"))
                {
                    var eventReceiver = Identity.GetEventReceiverOnList(list);
                    if (eventReceiver != null)
                    {
                        if (Force || (MyInvocation.BoundParameters.ContainsKey("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue($"Remove Event Receiver {eventReceiver.ReceiverName} with id {eventReceiver.ReceiverId}?", "Confirm"))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
                else
                {
                    var eventReceivers = new RestRequest(Context,$"{list.ObjectPath}/EventReceivers").Get<ResponseCollection<EventReceiverDefinition>>().Items;
                    foreach (var eventReceiver in eventReceivers)
                    {
                        if (Force || (MyInvocation.BoundParameters.ContainsKey("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue($"Remove Event Receiver {eventReceiver.ReceiverName} with id {eventReceiver.ReceiverId}?", "Confirm"))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
            }
            else
            {
                if (MyInvocation.BoundParameters.ContainsKey("Identity"))
                {
                    var eventReceiver = Identity.GetEventReceiverOnWeb(Context);
                    if (eventReceiver != null)
                    {
                        if (Force || (MyInvocation.BoundParameters.ContainsKey("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue($"Remove Event Receiver {eventReceiver.ReceiverName} with id {eventReceiver.ReceiverId}?", "Confirm"))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
                else
                {
                    var eventReceivers = new RestRequest(Context, $"{Context.Web.ObjectPath}/EventReceivers").Get<ResponseCollection<EventReceiverDefinition>>().Items;
                    
                    foreach (var eventReceiver in eventReceivers)
                    {
                        if (Force || (MyInvocation.BoundParameters.ContainsKey("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue($"Remove Event Receiver {eventReceiver.ReceiverName} with id {eventReceiver.ReceiverId}?", "Confirm"))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
            }

            if (eventReceiversToDelete.Count == 0)
            {
                WriteVerbose("No Event Receivers to remove");
                return;
            }

            foreach(var receiverToDelete in eventReceiversToDelete)
            {
                //bug in URL
                var objectPath = receiverToDelete.ObjectPath.Replace("/EventReceiver/", "/EventReceivers/");
                new RestRequest(Context, objectPath).Delete();
            }
        }
    }
}


