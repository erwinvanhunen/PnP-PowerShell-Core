using SharePointPnP.PowerShell.Core.ClientSidePages;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Linq;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public sealed class ClientSideComponentPipeBind
    {
        private readonly ClientSideComponent _component;
        private string _name;
        private Guid _id;

        public ClientSideComponentPipeBind(ClientSideComponent component)
        {
            _component = component;
            _id = _component.Id;
            _name = _component.Name;
        }

        public ClientSideComponentPipeBind(string nameOrId)
        {
            _component = null;
            if (!Guid.TryParse(nameOrId, out _id))
            {
                _name = nameOrId;
            }
        }

        public ClientSideComponentPipeBind(Guid id)
        {
            _id = id;
            _name = null;
            _component = null;
        }

        public ClientSideComponent Component => _component;

        public string Name => _component?.Name;

        public Guid Id => _component == null ? Guid.Empty : _component.Id;

        public override string ToString() => Name;

        internal ClientSideComponent GetComponent(SPOnlineContext context)
        {
            if (_component != null)
            {
                return _component;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                ClientSideComponent com = new RestRequest(context, "Web/GetClientSideWebParts").Get<ResponseCollection<ClientSideComponent>>().Items.FirstOrDefault(p => p.Name == _name || p.Alias == _name);
                return com;
            }
            else if (_id != Guid.Empty)
            {
                ClientSideComponent com = new RestRequest(context, "Web/GetClientSideWebParts").Get<ResponseCollection<ClientSideComponent>>().Items.FirstOrDefault(p => p.Id == Id);
                return com;
            }
            else
            {
                return null;
            }
        }
    }
}