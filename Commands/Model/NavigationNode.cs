using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class NavigationNode : ClientSideObject
    {
        [JsonProperty("odata.editLink")]
        internal string _editLink { get; set; }

        [JsonProperty("Id")]
        private int _id { get; set; }

        public List<NavigationNode> Children { get; set; }

        public bool IsDocLib { get; set; }
        public bool IsExternal { get; set; }
        public bool IsVisible { get; set; }
        public int ListTemplateType { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        [JsonIgnore]
        public int Id
        {
            get { return _id; }
        }
    }
}
