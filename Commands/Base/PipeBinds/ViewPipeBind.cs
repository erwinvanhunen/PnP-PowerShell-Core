using System;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public class ViewPipeBind
    {
        private readonly View _view;
        private readonly Guid _id;
        private readonly string _name;

        public ViewPipeBind()
        {
            _view = null;
            _id = Guid.Empty;
            _name = string.Empty;
        }

        public ViewPipeBind(View view)
        {
            _view = view;
        }

        public ViewPipeBind(Guid guid)
        {
            _id = guid;
        }

        public ViewPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _name = id;
            }
        }

        public Guid Id => _id;

        public View View => _view;

        public string Title => _name;

        public View GetView(SPOnlineContext context, List list)
        {
            if(_view != null)
            {
                return _view;
            }
            if(_id != Guid.Empty)
            {
                return new RestRequest(context, $"{list.ObjectPath}/views/getbyid('{_id}')").Get<View>();
            }
            if(!string.IsNullOrEmpty(_name))
            {
                return new RestRequest(context, $"{list.ObjectPath}/views/getbytitle('{_name}')").Get<View>();
            }
            return null;
        }
    }
}
