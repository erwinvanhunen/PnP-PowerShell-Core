using Newtonsoft.Json;
using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class UserCustomAction
    {
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
        public Rights Rights { get; set; }
        public string Title { get; set; }
        public string UrlAction { get; set; }
    }

    public class Rights
    {
        public string High { get; set; }
        public string Low { get; set; }
    }

}