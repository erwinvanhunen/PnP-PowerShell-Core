using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public sealed class EventReceiverPipeBind
    {
        private readonly EventReceiver _eventReceiver;
        private readonly Guid _id;
        private readonly string _name;

        public EventReceiverPipeBind()
        {
            _eventReceiver = null;
            _id = Guid.Empty;
            _name = string.Empty;
        }

        public EventReceiverPipeBind(EventReceiver eventReceiver)
        {
            _eventReceiver = eventReceiver;
        }

        public EventReceiverPipeBind(Guid guid)
        {
            _id = guid;
        }

        public EventReceiverPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _name = id;
            }
        }

        public Guid Id => _id;

        public EventReceiver EventReceiver => _eventReceiver;

        public string Name => _name;

        public override string ToString()
        {
            return Name ?? Id.ToString();
        }

        internal EventReceiver GetEventReceiverOnWeb(SPOnlineConnection context)
        {
            if (_eventReceiver != null)
            {
                return _eventReceiver;
            }

            if (_id != Guid.Empty)
            {
                return new RestRequest(context, $"Web/EventReceivers/GetById(guid'{_id}')").Get<EventReceiver>();
                            }
            else if (!string.IsNullOrEmpty(Name))
            {
                return new RestRequest(context, $"Web/EventReceivers").Get<ResponseCollection<EventReceiver>>().Items.FirstOrDefault(e => e.ReceiverName == Name);
            }
            return null;
        }

        internal EventReceiver GetEventReceiverOnList(List list)
        {
            if (_eventReceiver != null)
            {
                return _eventReceiver;
            }

            if (_id != Guid.Empty)
            {
                return new RestRequest(list.Context, $"Web/Lists/GetById(guid'{list.Id}')/EventReceivers/GetById(guid'{_id}')").Get<EventReceiver>();
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                return new RestRequest(list.Context, $"Web/Lists/GetById(guid'{list.Id}')/EventReceivers')").Get<ResponseCollection<EventReceiver>>().Items.FirstOrDefault(e => e.ReceiverName == Name);
            }
            return null;
        }
    }
}
