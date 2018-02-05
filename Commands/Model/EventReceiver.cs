using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class EventReceiver : ClientSideObject
    {
        public string ReceiverAssembly { get; set; }
        public string ReceiverClass { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public int SequenceNumber { get; set; }
        public int Synchronization { get; set; }
        public int EventType { get; set; }
        public object ReceiverUrl { get; set; }
    }
}
