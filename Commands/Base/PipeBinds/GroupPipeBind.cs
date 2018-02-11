using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public sealed class GroupPipeBind
    {
        private readonly int _id = -1;
        private readonly Group _group;
        private readonly string _name;
        public int Id => _id;

        public Group Group => _group;

        public string Name => _name;

        internal GroupPipeBind()
        {
        }

        public GroupPipeBind(int id)
        {
            _id = id;
        }

        public GroupPipeBind(Group group)
        {
            _group = group;
        }

        public GroupPipeBind(string name)
        {
            _name = name;
        }

        internal Group GetGroup(SPOnlineContext context)
        {
            Group group = null;
            if (Id != -1)
            {
                group = new RestRequest(context,$"Web/SiteGroups/GetById({Id})").Get<Group>();
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                group = new RestRequest(context, $"Web/SiteGroups/GetByName('{Name}')").Get<Group>();
            }
            else if (Group != null)
            {
                group = Group;
            }

            return group;
        }
    }
}
