using SharePointPnP.PowerShell.Core.ClientSidePages;
using SharePointPnP.PowerShell.Core.Model;
using System;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public sealed class ClientSidePagePipeBind
    {
        private readonly ClientSidePage _page;
        private string _name;

        public ClientSidePagePipeBind(ClientSidePage page)
        {
            _page = page;
            if (page.PageListItem != null)
            {
                File file = new RestRequest(page.Context, $"{page.PageListItem.ObjectPath}/File").Get<File>();
            }
            else
            {
                _name = page.PageTitle;
            }
        }

        public ClientSidePagePipeBind(string name)
        {
            _page = null;
            _name = name;
        }

        public ClientSidePage Page => _page;

        public string Name => EnsureCorrectPageName(_name);

        public override string ToString() => Name;

        internal ClientSidePage GetPage(SPOnlineContext ctx)
        {
            if (_page != null)
            {
                return _page;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                try
                {
                    return ClientSidePage.Load(ctx, Name);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public string EnsureCorrectPageName(string pageName)
        {
            if (pageName != null && !pageName.EndsWith(".aspx"))
                pageName += ".aspx";

            return pageName;
        }
    }
}