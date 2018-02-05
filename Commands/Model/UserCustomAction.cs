using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Enums;
using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class UserCustomAction
    {
        [JsonProperty("Scope")]
        private int _scope { get; set; }

        public Guid Id { get; set; }

        public string ClientSideComponentId { get; set; }
        public string ClientSideComponentProperties { get; set; }
        public string CommandUIExtension { get; set; }
        public string EnabledScript { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string RegistrationId { get; set; }
        public int RegistrationType { get; set; }
        public bool RequireSiteAdministrator { get; set; }
        public BasePermissions Rights { get; set; }
        public string Title { get; set; }
        public string UrlAction { get; set; }
        public string Name { get; set; }

        [JsonIgnore]

        public CustomActionScope Scope
        {
            get
            {
                return (CustomActionScope)_scope;
            }
            set
            {
                _scope = (int)value;
            }
        }
    }
}