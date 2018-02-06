using System;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
  public sealed class ListPipeBind
    {
        private readonly List _list;
        private readonly Guid _id;
        private readonly string _name;

        public ListPipeBind()
        {
            _list = null;
            _id = Guid.Empty;
            _name = string.Empty;
        }

        public ListPipeBind(List list)
        {
            _list = list;
        }

        public ListPipeBind(Guid guid)
        {
            _id = guid;
        }

        public ListPipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _name = id;
            }
        }

        public Guid Id => _id;

        public List List => _list;

        public string Title => _name;

        public override string ToString()
        {
            return Title ?? Id.ToString();
        }

        internal List GetList(SPOnlineConnection context)
        {
            List list = null;
            if (List != null)
            {
                list = List;
            }
            else if (Id != Guid.Empty)
            {
                list = new RestRequest(context, $"Lists(guid'{Id.ToString("D")}')").Expand("RootFolder/ServerRelativeUrl","ContentTypes").Get<List>();
            }
            else if (!string.IsNullOrEmpty(Title))
            {
                list = new RestRequest(context, $"Lists/GetByTitle('{Title}')").Expand("RootFolder/ServerRelativeUrl","ContentTypes").Get<List>();
            }
            return list;
        }
    }
}