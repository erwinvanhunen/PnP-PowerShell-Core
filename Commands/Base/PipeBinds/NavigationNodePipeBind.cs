using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public class NavigationNodePipeBind
    {
        private int _id;

        public NavigationNodePipeBind(int id)
        {
            _id = id;
        }

        public NavigationNodePipeBind(NavigationNode node)
        {
            _id = node.Id;
        }

        public int Id => _id;
    }
}
